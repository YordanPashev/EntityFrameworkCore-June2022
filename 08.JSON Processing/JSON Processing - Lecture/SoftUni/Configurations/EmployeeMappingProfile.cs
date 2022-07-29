namespace SoftUni.Configurations
{

    using AutoMapper;
    using SoftUni.Infrastructure.DTOs;

    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
