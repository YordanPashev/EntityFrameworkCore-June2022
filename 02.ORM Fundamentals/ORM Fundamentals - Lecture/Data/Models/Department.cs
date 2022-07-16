// ReSharper disable InconsistentNaming
namespace ORM_Fundamentals.Data.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Department
    {
        public Department()
        {
            //BackUp -> Improve Performance!!!
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //Navigational Properties are good to be virtual (LazyLoading)
        //All Employees that work in the current Department
        //One Relation
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
