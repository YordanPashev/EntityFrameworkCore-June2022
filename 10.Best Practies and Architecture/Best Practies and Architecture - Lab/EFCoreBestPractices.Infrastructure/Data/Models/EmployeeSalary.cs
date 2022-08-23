namespace EFCoreBestPractices.Infrastructure.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Keyless]
    public partial class EmployeeSalary
    {
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "decimal(15, 4)")]
        public decimal Salary { get; set; }
    }
}
