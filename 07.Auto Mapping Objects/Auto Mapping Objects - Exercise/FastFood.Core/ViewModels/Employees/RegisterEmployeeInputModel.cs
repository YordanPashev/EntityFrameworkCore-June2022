using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Employees
{
    public class RegisterEmployeeInputModel
    {
        [Required]
        [Range(15, 80)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Address { get; set; }
    }
}
