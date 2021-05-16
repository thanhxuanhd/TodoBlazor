using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.App.Models;

namespace Todo.App.Services.Profiles
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<CategoryListVm, CategoryViewModel>().ReverseMap();
            CreateMap<TodoVmPaginatedList, PaginatedTodoListViewModel>().ReverseMap();
            CreateMap<TodoVm, TodoViewModel>().ReverseMap();
        }
    }
}
