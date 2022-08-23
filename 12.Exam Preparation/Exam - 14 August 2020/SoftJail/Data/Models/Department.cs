namespace SoftJail.Data.Models
{
    using SoftJail.Common;

    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    public class Department
    {
        public Department()
        {
            Cells = new List<Cell>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DepartmenNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
