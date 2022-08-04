namespace SoftJail.DataProcessor.ImportDto
{

    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    [JsonObject]
    public class ImportMailDto
    {
        [Required]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("Sender")]
        public string Sender { get; set; }

        [Required]
        [RegularExpression("^[0-9A-Za-z\\s]+ str.$")]
        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
