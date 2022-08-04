namespace SoftJail.DataProcessor
{

    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;

    using AutoMapper;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using System.ComponentModel.DataAnnotations;  

    using Data;
    using XmlFacade;
    using SoftJail.DataProcessor.ImportDto;
    using SoftJail.Data.Models.Enums;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            ImportDepartmentDto[] departmentsDto = JsonConvert.DeserializeObject<ImportDepartmentDto[]>(jsonString);
            List<Department> validDepartments = new List<Department>();

            StringBuilder result = new StringBuilder();

            foreach (ImportDepartmentDto departmentDto in departmentsDto)
            {
                if (!IsValid(departmentDto) || 
                    !departmentDto.Cells.Any() ||
                    !departmentDto.Cells.All(IsValid))
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                    Department department = Mapper.Map<Department>(departmentDto);
                    validDepartments.Add(department);
                    result.AppendLine($"Imported {departmentDto.Name} with {departmentDto.Cells.Count} cells");
            }

            context.Departments.AddRange(validDepartments);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            ImportPrisonerDto[] prisonersDto = JsonConvert.DeserializeObject<ImportPrisonerDto[]>(jsonString);
            List<Prisoner> validPrisoners = new List<Prisoner>();
            StringBuilder result = new StringBuilder();

            foreach (ImportPrisonerDto prisonerDto in prisonersDto)
            {
                if (!IsValid(prisonerDto) ||
                    !prisonerDto.Mails.All(IsValid))
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                var isValidReleasedDate = DateTime.TryParseExact(prisonerDto.ReleaseDate, 
                                                                 "dd/MM/yyyy",
                                                                 CultureInfo.InvariantCulture, 
                                                                 DateTimeStyles.None, 
                                                                 out DateTime releasedDate);

                Prisoner prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = DateTime.ParseExact(prisonerDto.IncarcerationDate,
                                                            "dd/MM/yyyy",
                                                            CultureInfo.InvariantCulture),
                    ReleaseDate = isValidReleasedDate
                                  ? (DateTime?)releasedDate
                                  : null,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId,
                    Mails = Mapper.Map<Mail[]>(prisonerDto.Mails)

                }; 

                validPrisoners.Add(prisoner);
                result.AppendLine($"Imported {prisonerDto.FullName} {prisonerDto.Age} years old");
            }

            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            string rootElement = "Officers";
            ImportOfficerDto[] officersDto = XMLConverter.Deserializer<ImportOfficerDto>(xmlString, rootElement);
            List<Officer> validOfficers = new List<Officer>();

            StringBuilder result = new StringBuilder();

            foreach (ImportOfficerDto officerDto in officersDto)
            {
                if (!IsValid(officerDto))
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                Officer officer = new Officer()
                {
                    FullName = officerDto.FullName,
                    Salary = officerDto.Salary,
                    Weapon = Enum.Parse<Weapon>(officerDto.Weapon),
                    Position = Enum.Parse<Position>(officerDto.Position),
                    DepartmentId = officerDto.DepartmentId,
                    OfficerPrisoners = officerDto.Prisoners.Select(x => new OfficerPrisoner
                    {
                        PrisonerId = x.PrisonerId
                    })
                    .ToList()
                };
                validOfficers.Add(officer);
                result.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(validOfficers);
            context.SaveChanges(true);

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}
