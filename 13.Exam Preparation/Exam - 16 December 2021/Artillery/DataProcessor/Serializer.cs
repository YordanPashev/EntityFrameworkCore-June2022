namespace Artillery.DataProcessor
{
    using System.Linq;

    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;

    using XmlFacade;
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            ExportShellDto[] shellsDto = context.Shells
                .Include(s => s.Guns)
                .Where(s => s.ShellWeight > shellWeight)
                .ToArray()
                .Select(s => new ExportShellDto
                {
                    ShellWeight = s.ShellWeight,
                    Caliber = s.Caliber,
                    Guns = s.Guns.Where(g => g.GunType == GunType.AntiAircraftGun)
                                 .Select(g => new ExportGunDto
                    {
                        GunType = g.GunType.ToString(),
                        GunWeight = g.GunWeight,
                        BarrelLength = g.BarrelLength,
                        Range = g.Range > 3000 ? "Long-range"
                                               : "Regular range"
                    })
                    .OrderByDescending(g => g.GunWeight)
                    .ToArray()
                })
                .OrderBy(s => s.ShellWeight)
                .ToArray();

            string jsonResult = JsonConvert.SerializeObject(shellsDto, Formatting.Indented);
            return jsonResult.ToString().TrimEnd();
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            ExportGunInfoDto[] gunsDto = context.Guns
                .Include(g => g.Manufacturer)
                .Include(g => g.CountriesGuns)
                .ThenInclude(g => g.Country)
                .Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                .ToArray()
                .Select(g => new ExportGunInfoDto
                {
                    Manufacturer = g.Manufacturer.ManufacturerName,
                    GunType = g.GunType.ToString(),
                    GunWeight = g.GunWeight,
                    BarrelLength = g.BarrelLength,
                    Range = g.Range,
                    Countries = g.CountriesGuns.Where(cg => cg.Country.ArmySize > 4500000)
                                               .Select(c => new ExportCountryDto
                                               {
                                                   Countryname = c.Country.CountryName,
                                                   ArmySize = c.Country.ArmySize
                                               })
                                               .OrderBy(c => c.ArmySize)
                                               .ToArray()
                })
                .OrderBy(g => g.BarrelLength)
                .ToArray();

            string rootElement = "Guns";

            string xmlResult = XMLConverter.Serialize(gunsDto, rootElement);

            return xmlResult.ToString().TrimEnd();
        }
    }
}
