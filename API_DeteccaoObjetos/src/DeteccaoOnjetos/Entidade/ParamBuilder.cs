using DeteccaoOnjetos.Enum;
using System.Collections.Generic;

namespace DeteccaoOnjetos.Entidade
{
    public class ParamBuilder
    {
        private string Imagem { get; set; }
        private FeatureType FeatureType { get; set; }
        public ParamBuilder ComImagem(string imagem)
        {
            this.Imagem = imagem;
            return this;
        }

        public ParamBuilder ComFeatureType(FeatureType feature)
        {
            this.FeatureType = feature;
            return this;
        }

        public ParametroGoogleAPI Construir()
        {
            List<Feature> feature = new List<Feature>();
            feature.Add(new Feature() { Type = this.FeatureType.ToString() });

            ImagemParametro imagem = new ImagemParametro();
            imagem.Content = this.Imagem;

            ParametroGoogleAPI param = new ParametroGoogleAPI();
            param.Requests.Add(new Request()
            {
                Image = imagem,
                Features = feature
            });

            return param;
        }
    }
}
