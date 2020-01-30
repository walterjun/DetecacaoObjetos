using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeteccaoOnjetos.Entidade
{
    public class ParametroGoogleAPI
    {
        [JsonProperty("requests")]
        public List<Request> Requests { get; set; }

        public ParametroGoogleAPI()
        {
            this.Requests = new List<Request>();
        }
    }

    public class Request
    {
        [JsonProperty("image")]
        public ImagemParametro Image { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        public Request()
        {
            this.Features = new List<Feature>();
        }
    }

    public class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ImagemParametro
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
