using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApp.API.Dtos;
using TodoApp.API.Repositories;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();
            return Ok(result);

        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.Get(id);
            if (result == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Insert(InsertTodoDto dto)
        {
            var result = await _repository.Insert(dto);

            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(UpdateTodoDto dto)
        {
            var result = await _repository.Update(dto);

            if (result != null)
                return Ok(result);

            return NotFound(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);

            if(result)
                return Ok();

            return NotFound();
        }
    }
}
