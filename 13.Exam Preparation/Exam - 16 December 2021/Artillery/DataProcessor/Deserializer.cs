namespace Artillery.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Newtonsoft.Json;

    using XmlFacade;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data.";
        private const string SuccessfulImportCountry = "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer = "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell = "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun = "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            string rootElement = "Countries";
            ImportCountryDto[] countriesDto = XMLConverter.Deserializer<ImportCountryDto>(xmlString, rootElement);
            List<Country> validCountries = new List<Country>();
            StringBuilder result = new StringBuilder();

            foreach (ImportCountryDto countryDto in countriesDto)
            {
                if (!IsValid(countryDto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Country country = Mapper.Map<Country>(countryDto);

                validCountries.Add(country);
                result.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }

            context.Countries.AddRange(validCountries);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            string rootElement = "Manufacturers";
            ImportManufactorerDto[] manufactorersDto = XMLConverter.Deserializer<ImportManufactorerDto>(xmlString, rootElement);
            List<Manufacturer> validManufactorers = new List<Manufacturer>();
            StringBuilder result = new StringBuilder();

            foreach (ImportManufactorerDto manufactorerDto in manufactorersDto)
            {
                if (!IsValid(manufactorerDto) ||
                    validManufactorers.Any
                    (m => m.ManufacturerName == manufactorerDto.ManufacturerName))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer manufactorer = Mapper.Map<Manufacturer>(manufactorerDto);

                validManufactorers.Add(manufactorer);
                string manufacturerFoundingLocation = ExtractLocation(manufactorer.Founded);
                result.AppendLine(string.Format(SuccessfulImportManufacturer, manufactorer.ManufacturerName, manufacturerFoundingLocation));
            }

            context.Manufacturers.AddRange(validManufactorers);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            string rootElement = "Shells";
            ImportShellDto[] shellsDto = XMLConverter.Deserializer<ImportShellDto>(xmlString, rootElement);
            List<Shell> validShells= new List<Shell>();
            StringBuilder result = new StringBuilder();

            foreach (ImportShellDto shellDto in shellsDto)
            {
                if (!IsValid(shellDto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Shell shell = Mapper.Map<Shell>(shellDto);

                validShells.Add(shell);
                result.AppendLine(string.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
            }

            context.Shells.AddRange(validShells);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            ImportGunDto[] gunsDto = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);
            List<Gun> validGuns = new List<Gun>();
            StringBuilder result = new StringBuilder();

            foreach (ImportGunDto gunDto in gunsDto)
            {
                bool isGunTypeValid = Enum.TryParse<GunType>(gunDto.GunType, out GunType gunTypeDto);

                if (!IsValid(gunDto) || !isGunTypeValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                Gun gun = new Gun()
                {
                    ManufacturerId = gunDto.ManufacturerId,
                    GunWeight = gunDto.GunWeight,
                    BarrelLength = gunDto.BarrelLength,
                    NumberBuild = gunDto.NumberBuild,
                    Range = gunDto.Range,
                    GunType = gunTypeDto,
                    ShellId = gunDto.ShellId,
                    CountriesGuns = gunDto.Countries.Select(c => new CountryGun
                    {
                        CountryId = c.CountryId
                    })
                    .ToArray(),
                };

                validGuns.Add(gun);
                result.AppendLine(string.Format(SuccessfulImportGun, gun.GunType, gun.GunWeight, gun.BarrelLength));
            }

            context.Guns.AddRange(validGuns);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validator = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }

        private static string ExtractLocation(string founded)
        {
            string[] allFoundedData = founded.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            int lastElementIndex = allFoundedData.Length - 1;
            string countryName = allFoundedData[lastElementIndex];
            string townName = allFoundedData[lastElementIndex - 1];

            return $"{townName}, {countryName}".TrimEnd();
        }
    }
}
