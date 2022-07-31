namespace CarDealer.DTO.Car
{

    using System.Collections.Generic;

    using CarDealer.DTO.Part;

    public class ExportCarFullInfoDTO
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public IEnumerable<ExportPartNameAndPriceDTO> Parts { get; set; } = new List<ExportPartNameAndPriceDTO>();
    }
}
