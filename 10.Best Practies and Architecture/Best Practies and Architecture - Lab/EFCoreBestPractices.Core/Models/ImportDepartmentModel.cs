namespace EFCoreBestPractices.Core.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    public class ImportDepartmentModel
    {
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [Column("ManagerID")]
        public int ManagerId { get; set; }
    }
}
