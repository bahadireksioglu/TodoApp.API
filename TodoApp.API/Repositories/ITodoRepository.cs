using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.API.Dtos;
using TodoApp.API.Entities;

namespace TodoApp.API.Repositories
{
    public interface ITodoRepository
    {
        public Task<IList<Todo>> GetAll();
        public Task<Todo> Get(int id);
        public Task<Todo> Insert(InsertTodoDto dto);
        public Task<Todo> Update(UpdateTodoDto dto);
        public Task<bool> Delete(int id);

    }
}
