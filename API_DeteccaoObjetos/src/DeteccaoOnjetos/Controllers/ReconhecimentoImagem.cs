using DeteccaoOnjetos.Entidade;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeteccaoOnjetos.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MinhaPolitica")]
    public class ReconhecimentoImagem : Controller
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<List<string>> Post(IFormFile file)
        {
            byte[] imagemBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                imagemBytes = ms.ToArray();
            }

            ParamBuilder param = new ParamBuilder();
            var json = param.ComImagem(Convert.ToBase64String(imagemBytes))
                            .ComFeatureType(Enum.FeatureType.WEB_DETECTION)
                            .Construir();

            RespostaAPIGoogle resposta = await this.Post<RespostaAPIGoogle>("images:annotate", json);

            List<string> listaRetorno = new List<string>();

            if(resposta != null && resposta.Responses.Count > 0)
                resposta.Responses.FirstOrDefault().
                    WebDetection.BestGuessLabels.ForEach(x => listaRetorno.Add(x.Label));

            return listaRetorno;
        }

        private async Task<T> Post<T>(string metodo, object param)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), System.Text.Encoding.UTF8, "application/json");
                string url = $"https://vision.googleapis.com/v1/{metodo}?key=AIzaSyDL9MSg9aOlEW-XkopNT0wzD1rW_qTVias";
                var resposta = await client.PostAsync(url, content);

                if (resposta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resultado = resposta.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(resultado.ToString());
                }

                return default(T);
            }
        }
    }
}
