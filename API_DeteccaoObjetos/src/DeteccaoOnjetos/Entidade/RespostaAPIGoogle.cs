using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DeteccaoOnjetos.Entidade
{
    public partial class RespostaAPIGoogle
    {
        [JsonProperty("responses")]
        public List<Response> Responses { get; set; }
    }

    public class Response
    {
        [JsonProperty("webDetection")]
        public WebDetection WebDetection { get; set; }
    }

    public class WebDetection
    {
        [JsonProperty("webEntities")]
        public List<WebEntity> WebEntities { get; set; }

        [JsonProperty("fullMatchingImages")]
        public List<Image> FullMatchingImages { get; set; }

        [JsonProperty("partialMatchingImages")]
        public List<Image> PartialMatchingImages { get; set; }

        [JsonProperty("pagesWithMatchingImages")]
        public List<PagesWithMatchingImage> PagesWithMatchingImages { get; set; }

        [JsonProperty("visuallySimilarImages")]
        public List<Image> VisuallySimilarImages { get; set; }

        [JsonProperty("bestGuessLabels")]
        public List<BestGuessLabel> BestGuessLabels { get; set; }
    }

    public class BestGuessLabel
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public class PagesWithMatchingImage
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("pageTitle")]
        public string PageTitle { get; set; }

        [JsonProperty("fullMatchingImages", NullValueHandling = NullValueHandling.Ignore)]
        public List<Image> FullMatchingImages { get; set; }

        [JsonProperty("partialMatchingImages", NullValueHandling = NullValueHandling.Ignore)]
        public List<Image> PartialMatchingImages { get; set; }
    }

    public class WebEntity
    {
        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public partial class RespostaAPIGoogle
    {
        public static RespostaAPIGoogle FromJson(string json) => JsonConvert.DeserializeObject<RespostaAPIGoogle>(json);
    }

    public static class Serialize
    {
        public static string ToJson(this RespostaAPIGoogle self) => JsonConvert.SerializeObject(self);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
