namespace SoftJail.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SoftJail.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Prisoner
    {
        public Prisoner()
        {
            Mails = new List<Mail>();
            PrisonerOfficers = new List<OfficerPrisoner>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PrisonerFullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public DateTime IncarcerationDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        [ForeignKey(nameof(Cell))]
        public int? CellId { get; set; }
        public Cell Cell { get; set; }

        public ICollection<Mail> Mails { get; set; }

        public ICollection<OfficerPrisoner> PrisonerOfficers { get; set; }
    }
}