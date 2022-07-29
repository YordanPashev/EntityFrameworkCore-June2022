namespace ProductShop.DTOs.User
{

    using AutoMapper;
    using ProductShop.Common;
    using System.ComponentModel.DataAnnotations;

    public class ImportUserDTO : Profile
    {
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.UserLastNameMinLength)]
        public string LastName { get; set; }

        public int? Age { get; set; }
    }
}
