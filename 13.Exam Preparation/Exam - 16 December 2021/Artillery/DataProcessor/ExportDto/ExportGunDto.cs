using System;
using System.Collections.Generic;
using System.Text;

namespace Artillery.DataProcessor.ExportDto
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportGunDto
    {
        [JsonProperty("GunType")]
        public string GunType { get; set; }

        [JsonProperty("GunWeight")]
        public int GunWeight { get; set; }

        [JsonProperty("BarrelLength")]
        public double BarrelLength { get; set; }

        [JsonProperty("Range")]
        public string Range { get; set; }
    }
}
