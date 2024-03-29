﻿namespace CarDealer.Models
{
    using System;
    using System.Collections.Generic;

    public class Customer
    {
        public Customer()
        {
            Sales = new List<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}