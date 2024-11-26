using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _service;

        public TurmaController(ITurmaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var turmas = await _service.GetAllAsync();
            return Ok(turmas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var turma = await _service.GetByIdAsync(id);
            if (turma == null)
                return NotFound();

            return Ok(turma);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Turma turma)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTurma = await _service.AddAsync(turma);
            return CreatedAtAction(nameof(GetById), new { id = createdTurma.Id }, createdTurma);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Turma turma)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, turma);
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
