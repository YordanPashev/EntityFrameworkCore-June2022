namespace SoftJail
{

    using AutoMapper;

    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            //Import Departments and Cells
            this.CreateMap<ImportCellDto, Cell>();
            this.CreateMap<ImportDepartmentDto, Department>();

            //Import Prisoners and Mails
            this.CreateMap<ImportMailDto, Mail>();

            //Import Officers and Prisoners
            this.CreateMap<ImportOfficerPrisonerDto, OfficerPrisoner>()
                .ForMember(d => d.PrisonerId, mo => mo.MapFrom(mo => mo.PrisonerId));
        }
    }
}
