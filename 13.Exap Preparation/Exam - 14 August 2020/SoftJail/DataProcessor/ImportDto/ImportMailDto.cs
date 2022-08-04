namespace SoftJail.DataProcessor.ImportDto
{

    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
     
    using SoftJail.Common;

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
        [RegularExpression(GlobalConstants.MailAddressRegex)]
        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
