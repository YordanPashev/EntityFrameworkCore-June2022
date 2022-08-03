namespace CarDealer.Dtos.Export
{

    using System.Xml.Serialization;

    [XmlType("sale")]
    public class ExportSaleWithDiscountInfoDTO
    {
        [XmlElement("car")]

        public ExportCarInfoDTO CarInfo { get; set; }

        [XmlElement("discount")]
        public double Discount { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("price")]
        public decimal SalePriceWithoutDiscount { get; set; }

        [XmlElement("price-with-discount")]
        public decimal SalePriceWithIncludedDiscount { get; set; }
    }
}