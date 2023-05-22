using AutoMapper;
using TaskTracker.Application.DTOs.Checklists;
using TaskTracker.Application.DTOs.Tasks;
using TaskTracker.Application.DTOs.Users;
using TaskTracker.Application.Models;
using TaskTracker.Domain;

namespace TaskTracker.Application.Profiles;
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region user Mappings

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<RegisterDto, CreateUserDto>().ReverseMap();
            CreateMap<RegistrationRequest,RegisterDto>().ReverseMap();

            #endregion user

            #region task Mappings

            CreateMap<task, TaskDto>().ReverseMap();
            CreateMap<task, CreateTaskDto>().ReverseMap();
            CreateMap<task, UpdateTaskDto>().ReverseMap();

            #endregion task
            
            #region checklist Mappings

            CreateMap<Checklist, CreateChecklistDto>().ReverseMap();
            CreateMap<Checklist, ChecklistDto>().ReverseMap();
            CreateMap<Checklist, UpdateChecklistDto>().ReverseMap();

            #endregion checklist

        }
    }

