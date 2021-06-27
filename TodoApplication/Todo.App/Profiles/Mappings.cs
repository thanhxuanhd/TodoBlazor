using AutoMapper;
using Todo.App.Models;

namespace Todo.App.Services.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CategoryListVm, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryVm, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryViewModel, CreateCategoryCommand>().ReverseMap();
            CreateMap<CategoryViewModel, UpdateCategoryCommand>().ReverseMap();
            CreateMap<CategoryViewModel, CreateCategoryDto>().ReverseMap();
            CreateMap<CreateCategoryCommandResponse, CreateCategoryResponse>().ReverseMap();
            CreateMap<UpdateCategoryCommandResponse, UpdateCategoryReponse>().ReverseMap();
            CreateMap<CategoryViewModel, UpdateCategoryDto>().ReverseMap();

            CreateMap<TodoVmPaginatedList, PaginatedTodoListViewModel>().ReverseMap();
            CreateMap<TodoVm, TodoViewModel>().ReverseMap();
            CreateMap<TodoVm, CreateTodoCommand>().ReverseMap();
            CreateMap<TodoViewModel, CreateTodoCommand>().ReverseMap();
            CreateMap<CreateTodoCommandResponse, CreateTodoResponse>().ReverseMap();
            CreateMap<UpdateTodoResponse, UpdateTodoCommandResponse>().ReverseMap();
            CreateMap<TodoVm, UpdateTodoCommand>().ReverseMap();
            CreateMap<TodoViewModel, UpdateTodoCommand>().ReverseMap();
            CreateMap<CreateTodoDto, TodoViewModel>().ReverseMap();
            CreateMap<UpdateTodoDto, TodoViewModel>().ReverseMap();
        }
    }
}