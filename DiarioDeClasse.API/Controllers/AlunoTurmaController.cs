using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoTurmaController : ControllerBase
    {
        private readonly IAlunoTurmaService _service;

        public AlunoTurmaController(IAlunoTurmaService service)
        {
            _service = service;
        }

        [HttpGet("Aluno/{alunoId:int}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            var alunoTurmas = await _service.GetByAlunoIdAsync(alunoId);
            return Ok(alunoTurmas);
        }

        [HttpGet("Turma/{turmaId:int}")]
        public async Task<IActionResult> GetByTurmaId(int turmaId)
        {
            var alunoTurmas = await _service.GetByTurmaIdAsync(turmaId);
            return Ok(alunoTurmas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoTurma alunoTurma)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var jaMatriculado = await _service.AlunoJaMatriculadoAsync(alunoTurma.AlunoId, alunoTurma.TurmaId);
            if (jaMatriculado)
                return Conflict("O aluno já está matriculado nesta turma.");

            await _service.AddAsync(alunoTurma);
            return CreatedAtAction(nameof(GetByAlunoId), new { alunoId = alunoTurma.AlunoId }, alunoTurma);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlunoTurma alunoTurma)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, alunoTurma);
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
