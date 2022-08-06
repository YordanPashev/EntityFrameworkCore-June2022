namespace Footballers.DataProcessor
{
    using System;
    using System.Linq;
    using System.Globalization;

    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using XmlFacade;
    using DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            ExportCoachDto[] coachesInfo = context.Coaches
                .Include(c => c.Footballers)
                .Where(c => c.Footballers.Any())
                .ToArray()
                .Select(c => new ExportCoachDto
                {
                    FootballersCount = c.Footballers.Count,
                    CoachName = c.Name,
                    Footballers = c.Footballers.Select(f => new ExportFootballerDto
                    {
                        Name = f.Name,
                        Position = f.PositionType.ToString()
                    })   
                    .OrderBy(f => f.Name)
                    .ToArray()
                })
                .OrderByDescending(c => c.FootballersCount)
                .ThenBy(c => c.CoachName)
                .ToArray();

            string rootName = "Coaches";
            string xmlResult = XMLConverter.Serialize(coachesInfo, rootName);

            return xmlResult.TrimEnd();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            ExportTeamInfoDto[] teamsInfoDto = context.Teams
                .Include(t => t.TeamsFootballers)
                .ThenInclude(tf => tf.Team)
                .Where(t => t.TeamsFootballers.Any(t => t.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new ExportTeamInfoDto
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers.Where(tf => tf.Footballer.ContractStartDate >= date)
                                                    .Select(tf => new ExportFullInfoForFootballerDto
                    {
                        FootballerName = tf.Footballer.Name,
                        ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = tf.Footballer.BestSkillType.ToString(),
                        PositionType = tf.Footballer.PositionType.ToString()
                     })
                    .OrderByDescending(t => DateTime.Parse(t.ContractEndDate))
                    .ThenBy(f => f.FootballerName)
                    .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Count())
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();

            string jsonResult = JsonConvert.SerializeObject(teamsInfoDto, Formatting.Indented);

            return jsonResult.TrimEnd();
        }
    }
}
