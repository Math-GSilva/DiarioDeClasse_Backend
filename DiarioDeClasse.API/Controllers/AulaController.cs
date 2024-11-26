using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaController : ControllerBase
    {
        private readonly IAulaService _service;

        public AulaController(IAulaService service)
        {
            _service = service;
        }

        [HttpGet("Turma/{turmaId:int}")]
        public async Task<IActionResult> GetByTurmaId(int turmaId)
        {
            var aulas = await _service.GetByTurmaIdAsync(turmaId);
            return Ok(aulas);
        }

        [HttpGet("Date/{date:datetime}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var aulas = await _service.GetByDateAsync(date);
            return Ok(aulas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Aula aula)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAula = await _service.AddAsync(aula);
            return CreatedAtAction(nameof(GetByTurmaId), new { turmaId = aula.TurmaId }, createdAula);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Aula aula)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, aula);
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
