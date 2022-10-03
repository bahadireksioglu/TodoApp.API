using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.API.Contexts;
using TodoApp.API.Dtos;
using TodoApp.API.Entities;

namespace TodoApp.API.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext _context;
        public TodoRepository(TodoAppDbContext context)
        {
            _context = context;
        } 
        public async Task<IList<Todo>> GetAll()
        {
            var count = await _context.Todos.CountAsync();
            
            if(count > 0)
                return await _context.Todos.ToListAsync();

            return new List<Todo>();
        }

        public async Task<Todo> Get(int id)
        {
            var any = await _context.Todos.AnyAsync(x=>x.Id == id);
            if(any)
                return await _context.Todos.FirstOrDefaultAsync(x=>x.Id == id);

            return null;
        }

        public async Task<Todo> Insert(InsertTodoDto dto)
        {
            var entity = new Todo
            {
                Title = dto.Title,
                Content = dto.Content,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            var insertedEntity = await _context.Todos.AddAsync(entity);
            await _context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public async Task<Todo> Update(UpdateTodoDto dto)
        {
            var any = await _context.Todos.AnyAsync(x => x.Id == dto.Id);

            if (any)
            {
                var entity = await _context.Todos.FirstOrDefaultAsync(x=>x.Id == dto.Id);
                entity.Title = dto.Title;
                entity.Content = dto.Content;
                entity.UpdatedDate = DateTime.Now;

                var updatedEntity = _context.Todos.Update(entity);

                await _context.SaveChangesAsync();

                return updatedEntity.Entity;
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var any = await _context.Todos.AnyAsync(x => x.Id == id);

            if (any)
            {
                var entity = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
                _context.Todos.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
