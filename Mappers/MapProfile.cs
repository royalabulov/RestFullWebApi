using AutoMapper;
using RestFullWebApi.DTOs.RoleDTOs;
using RestFullWebApi.DTOs.SchoolDTOs;
using RestFullWebApi.DTOs.StudentDTOs;
using RestFullWebApi.DTOs.UserDTOs;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Entity.ApplicationEntites;

namespace RestFullWebApi.Mappers
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<SchoolCreateDTO, School>().ReverseMap();
            CreateMap<SchoolGetDTO,School>().ReverseMap();
            CreateMap<SchoolUpdateDTO, School>().ReverseMap();

            CreateMap<StudentCreateDTO, Student>().ReverseMap();
            CreateMap<StudentGetDTO, Student>().ReverseMap();
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();

            CreateMap<RoleUpdateDTO, AppRole>().ReverseMap();
            CreateMap<RoleGetDTO, AppRole>().ReverseMap();

            CreateMap<UserDTO,AppUser>().ReverseMap();
            CreateMap<UserUpdateDTO, AppUser>().ReverseMap();
            CreateMap<CreateUserDto, AppUser>().ReverseMap();
            CreateMap<CreateUserResponseDTo, AppUser>().ReverseMap();

        }
    }
}
