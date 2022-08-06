namespace SoftJail.DataProcessor
{

    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using SoftJail.DataProcessor.ExportDto;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    using Data;
    using SoftJail.Data.Models;
    using XmlFacade;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            ExportPrisonerDto[] prisonersDto = context.Prisoners
                                                 .Where(p => ids.Contains(p.Id))
                                                 .Include(p => p.PrisonerOfficers)
                                                 .ThenInclude(po => po.Officer)
                                                 .ThenInclude(o => o.Department)
                                                 .Select(p => new ExportPrisonerDto()
                                                 {
                                                     Id = p.Id,
                                                     Name = p.FullName,
                                                     CellNumber = p.Cell.CellNumber,
                                                     Officers = p.PrisonerOfficers.Select(o => new ExportOfficerDto
                                                     {
                                                         Name = o.Officer.FullName,
                                                         Department = o.Officer.Department.Name
                                                     })
                                                     .OrderBy(o => o.Name)
                                                     .ToArray(),
                                                     TotalOfficerSalary = GetTotalOfficerSalaryForCurrPrisoner(p.PrisonerOfficers)
                                                 })
                                                 .OrderBy(p => p.Name)
                                                 .ThenBy(p => p.Id)
                                                 .ToArray();
            
            string jsonResult = JsonConvert.SerializeObject(prisonersDto, Formatting.Indented);
            return jsonResult;
        }
  
        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] selectedPisonersnames = prisonersNames
                                                     .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                     .ToArray();

            ExportInboxForPrisonerDto[] prisonersDto= context.Prisoners
                                                       .Include(p => p.Mails)
                                                       .Where(p => selectedPisonersnames.Contains(p.FullName))
                                                       .Select(p => new ExportInboxForPrisonerDto
                                                       {
                                                           Id = p.Id,
                                                           Name = p.FullName,
                                                           IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                                                           EncryptedMessages = p.Mails.Select(m => new ExportEncryptedPrisonerMailDto
                                                           {
                                                               Description = EncryptMessage(m.Description)
                                                           })
                                                           .ToArray()
                                                       
                                                       })
                                                       .OrderBy(p => p.Name)
                                                       .ThenBy(p => p.Id)
                                                       .ToArray();

            string rotElement = "Prisoners";
            string xmlResult = XMLConverter.Serialize(prisonersDto, rotElement);
            return xmlResult;
        }

        private static string EncryptMessage(string description)
        {
            char[] charArray = description.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static decimal GetTotalOfficerSalaryForCurrPrisoner(ICollection<OfficerPrisoner> prisonerOfficers)
            => Math.Round(prisonerOfficers.Sum(po => po.Officer.Salary), 2);
    }
}