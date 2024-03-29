﻿namespace CarDealer.Dtos.Export
{

    using System.Xml.Serialization;

    [XmlType("part")]
    public class ExportPartNameAndPriceDTO
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
