using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using DiarioDeClasse.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _service;

        public ProfessorController(IProfessorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professores = await _service.GetAllAsync();
            return Ok(professores);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _service.GetByIdAsync(id);
            if (professor == null)
                return NotFound();

            return Ok(professor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Professor professor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(professor);
            return CreatedAtAction(nameof(GetById), new { id = professor.Id }, professor);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Professor professor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, professor);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
