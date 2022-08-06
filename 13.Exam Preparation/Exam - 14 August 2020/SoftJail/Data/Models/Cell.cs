namespace SoftJail.Data.Models
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cell
    {
        public Cell()
        {
            Prisoners = new List<Prisoner>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Prisoner> Prisoners { get; set; }
    }
}
