using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreBestPractices.Core.Models
{
    public class ImportDepartmentModel
    {
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [Column("ManagerID")]
        public int ManagerId { get; set; }
    }
}
