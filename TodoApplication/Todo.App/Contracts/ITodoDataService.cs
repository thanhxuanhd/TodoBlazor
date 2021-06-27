using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.App.Models;

namespace Todo.App.Contracts
{
    public interface ITodoDataService
    {
        public Task<PaginatedTodoListViewModel> GetTodos(int pageSize = 20, int pageIndex = 0, string keyword = "", bool isCompletedOnly = false, Guid? categoryId = null);

        public Task<TodoViewModel> GetTodo(Guid todoId);


        public Task<CreateTodoResponse> CreateTodo(TodoViewModel todo);

        public Task<UpdateTodoResponse> UpdateTodo(TodoViewModel todo);

        public Task<bool> DeletedTodo(Guid todoId);
    }
}
