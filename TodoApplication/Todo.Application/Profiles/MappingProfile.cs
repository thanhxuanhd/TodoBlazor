using AutoMapper;
using Todo.Application.Contracts;
using Todo.Application.Features.Categories.Commands.CreateCategory;
using Todo.Application.Features.Categories.Commands.UpdateCategory;
using Todo.Application.Features.Categories.Queries.GetCategoriesList;
using Todo.Application.Features.Categories.Queries.GetCategoryDetail;
using Todo.Application.Features.Todo.Queries.GetTodoList;
using Entities = Todo.Domain.Entities;

namespace Todo.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Todo, TodoVm>().ReverseMap();
            CreateMap<PaginatedList<Entities.Todo>, PaginatedList<TodoVm>>().ReverseMap();

            CreateMap<Entities.Category, CategoryListVm>().ReverseMap();
            CreateMap<Entities.Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Entities.Category, CategoryVm>().ReverseMap();
            CreateMap<Entities.Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Entities.Category, UpdateCategoryCommand>().ReverseMap();
        }
    }
}