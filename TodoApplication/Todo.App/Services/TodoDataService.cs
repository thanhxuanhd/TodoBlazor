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

        public Task<Guid> CreateTodo(TodoViewModel todo)
        {
            throw new NotImplementedException();
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
    }
}