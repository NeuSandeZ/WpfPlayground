using AutoMapper;
using Hotel.Application.DTOS.TasksListingDto;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappings;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TasksAddDto, Tasks>()
            .ForMember(dest => dest.StaffId,
                opt => opt.MapFrom(
                    src => src.StaffId))
            .ForMember(dest => dest.TaskStatusId,
                opt => opt.MapFrom(
                    src => src.TaskStatusId));
        
        CreateMap<Tasks, TasksListingDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(
                    src => src.TaskStatus.Status))
            .ForMember(dest => dest.AssignedTo,
                opt => opt.MapFrom(
                    src => src.Staff.FullName));
    }
}