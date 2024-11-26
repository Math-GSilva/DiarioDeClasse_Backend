using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController : ControllerBase
    {
        private readonly INotaService _service;

        public NotaController(INotaService service)
        {
            _service = service;
        }

        [HttpGet("Aluno/{alunoId:int}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            var notas = await _service.GetByAlunoIdAsync(alunoId);
            return Ok(notas);
        }

        [HttpGet("Turma/{turmaId:int}")]
        public async Task<IActionResult> GetByTurmaId(int turmaId)
        {
            var notas = await _service.GetByTurmaIdAsync(turmaId);
            return Ok(notas);
        }

        [HttpGet("Avaliacao/{avaliacao}")]
        public async Task<IActionResult> GetByAvaliacao(string avaliacao)
        {
            var notas = await _service.GetByAvaliacaoAsync(avaliacao);
            return Ok(notas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Nota nota)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdNota = await _service.AddAsync(nota);
            return CreatedAtAction(nameof(GetByAlunoId), new { alunoId = nota.AlunoId }, createdNota);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Nota nota)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, nota);
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
