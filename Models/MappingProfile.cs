using AutoMapper;
namespace Webapi.Models
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
        }
    }

}
