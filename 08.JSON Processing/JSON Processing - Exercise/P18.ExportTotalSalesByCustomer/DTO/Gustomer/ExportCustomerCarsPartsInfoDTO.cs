namespace CarDealer.DTO.Gustomer
{
    using CarDealer.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ExportCustomerCarsPartsInfoDTO
    {
        public string CustomerFullName { get; set; }

        public int CarsBoughtCount { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public int DiscountForYoungDriver { get; set; }
    }
}
