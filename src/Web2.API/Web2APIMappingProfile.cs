using AutoMapper;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API
{
    public class Web2APIMappingProfile : Profile
    {
        public Web2APIMappingProfile()
        {
            CreateMap<Categorie, CategorieDTO>();
            CreateMap<Ville, VilleDTO>();
            CreateMap<Evenement, EvenementDTO>();
            CreateMap<Participation, ParticipationDTO>();

            //CreateMap<Student, StudentDTO>()
            //    .ForMember(dest => dest.EnrollmentIds, opt => opt.MapFrom(src => src.Enrollments.Select(e => e.ID).ToList()));
            
            ////.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
            ////.ForMember(dest => dest.CourseID, opt => opt.MapFrom(src => src.Course.CourseID))
            ////.ForMember(dest => dest.Credits, opt => opt.MapFrom(src => src.Course.Credits));
        }
    }
}
