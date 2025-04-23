using AutoMapper;
using TaskManagement.Dtos;
using TaskManagement.Entities;

namespace TaskManagement;

public class TaskManagementApplicationAutoMapperProfile : Profile
{
    public TaskManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // CreateUpdateTaskDto -> TaskItem
        CreateMap<CreateUpdateTaskDto, TaskItem>().ReverseMap() ;

        // Category -> CategoryDto
        CreateMap<Category, CategoryDto>().ReverseMap();

        // CategoryDto -> Category (for Create)
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<TaskItem, TaskDto>().ReverseMap();
        
    }
}
