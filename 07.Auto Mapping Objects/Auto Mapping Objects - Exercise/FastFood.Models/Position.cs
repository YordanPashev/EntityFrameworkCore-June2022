namespace FastFood.Models
{
    using System.Collections.Generic;

    public class Position
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ICollection<Employee> Employees { get; set; }
	}
}