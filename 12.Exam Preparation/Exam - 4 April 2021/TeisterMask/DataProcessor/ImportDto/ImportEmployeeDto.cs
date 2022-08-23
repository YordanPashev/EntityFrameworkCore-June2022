namespace TeisterMask.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using TeisterMask.Common;

    [JsonObject]
    public class ImportEmployeeDto
    {
        [JsonProperty("Username")]
        [RegularExpression(EntityValidations.UserNameRgex)]
        [MinLength(EntityValidations.EmployeeUserNameMinLength)]
        [MaxLength(EntityValidations.EmployeeUserNameMaxLength)]
        public string Username { get; set; }

        [JsonProperty("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        [RegularExpression(EntityValidations.EmployeePhoneRegex)]
        public string Phone { get; set; }

        [JsonProperty("Tasks")]
        public int[] Tasks { get; set; }
    }
}
