using AutoMapper;
using System;
using System.Threading.Tasks;
using Todo.App.Contracts;
using Todo.App.Models;

namespace Todo.App.Services
{
    public class TodoDataService : BaseDataService, ITodoDataService
    {
        private readonly IMapper _mapper;

        public TodoDataService(IClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }

        public async Task<CreateTodoResponse> CreateTodo(TodoViewModel todo)
        {
            var todoMap = _mapper.Map<CreateTodoCommand>(todo);
            var createTodoResponse = await _client.CreateTodoAsync(todoMap);
            var mapCreateTodoResponse = _mapper.Map<CreateTodoResponse>(createTodoResponse);
            return mapCreateTodoResponse;
        }

        public async Task<TodoViewModel> GetTodo(Guid todoId)
        {
            var todo = await _client.GetTodoAsync(todoId);
            var mapTodo = _mapper.Map<TodoViewModel>(todo);
            return mapTodo;
        }

        public async Task<PaginatedTodoListViewModel> GetTodos(int pageSize = 20, int pageIndex = 0, string keyword = "", bool isCompletedOnly = false, Guid? categoryId = null)
        {
            var todos = await _client.GetTodosAsync(pageSize, pageIndex, keyword, isCompletedOnly, categoryId);
            var mapTodos = _mapper.Map<PaginatedTodoListViewModel>(todos);

            return mapTodos;
        }

        public async Task<UpdateTodoResponse> UpdateTodo(TodoViewModel todo)
        {
            var todoMap = _mapper.Map<UpdateTodoCommand>(todo);
            var updateTodoCommandResponse = await _client.UpdateTodoAsync(todoMap);
            var mapTodoResponse = _mapper.Map<UpdateTodoResponse>(updateTodoCommandResponse);
            return mapTodoResponse;
        }
    }
}