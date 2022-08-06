namespace Footballers.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using Data;
    using XmlFacade;
    using Data.Models.Enums;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfullyImportedCoach = "Successfully imported coach - {0} with {1} footballers.";
        private const string SuccessfullyImportedTeam = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            string rootElement = "Coaches";
            ImportCoachDto[] coachesDto = XMLConverter.Deserializer<ImportCoachDto>(xmlString, rootElement);
            StringBuilder result = new StringBuilder();
            List<Coach> validCoaches = new List<Coach>();

            foreach (ImportCoachDto coachDto in coachesDto)
            {
                if (!IsValid(coachDto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<Footballer> validTeamFootballers = new List<Footballer>();

                foreach (ImportFootballerDto footballerDto in coachDto.Footballers)
                {
                    if (!TryToAddFootBallerToTeamList(footballerDto, validTeamFootballers))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }
                }

                Coach coach = new Coach()
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality,
                    Footballers = validTeamFootballers
                };

                result.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
                validCoaches.Add(coach);
            }

            context.Coaches.AddRange(validCoaches);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            ImportTeamDto[] teamsDto = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);
            List<Team> validTeams = new List<Team>();
            StringBuilder result = new StringBuilder();

            foreach (ImportTeamDto teamDto in teamsDto)
            {
                if (!IsValid(teamDto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<TeamFootballer> footballersToAddToTheTeam = new List<TeamFootballer>();

                foreach (int footballerIdDto in teamDto.Footballers.Distinct())
                {
                    if (!context.Footballers.Any(f => f.Id == footballerIdDto))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }

                    TeamFootballer footaller = new TeamFootballer()
                    {
                        FootballerId = footballerIdDto,
                    };

                    footballersToAddToTheTeam.Add(footaller);
                }

                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies,
                    TeamsFootballers = footballersToAddToTheTeam
                };

                validTeams.Add(team);
                result.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(validTeams);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static bool TryToAddFootBallerToTeamList(ImportFootballerDto footballerDto, List<Footballer> validTeamFootballers)
        {
            if (!IsValid(footballerDto))
            {
                return false;
            }

            bool isConStartDateValid = DateTime.TryParseExact(footballerDto.ContractStartDate,
                                                              "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                              DateTimeStyles.None, out DateTime conStartDate);

            bool isConStartEndValid = DateTime.TryParseExact(footballerDto.ContractEndDate,
                                                             "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                              DateTimeStyles.None, out DateTime conEndDate);

            if (!isConStartDateValid || !isConStartEndValid)
            {
                return false;
            }

            if (conStartDate > conEndDate)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(BestSkillType), footballerDto.BestSkillType) ||
                !Enum.IsDefined(typeof(PositionType), footballerDto.PositionType))
            {
                return false;
            }

            Footballer footballer = new Footballer()
            {
                Name = footballerDto.Name,
                ContractStartDate = conStartDate,
                ContractEndDate = conEndDate,
                BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                PositionType = (PositionType)footballerDto.PositionType
            };

            validTeamFootballers.Add(footballer);
            return true;
        }
    }
}
