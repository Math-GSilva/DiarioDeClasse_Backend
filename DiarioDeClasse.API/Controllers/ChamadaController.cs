using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadaController : ControllerBase
    {
        private readonly IChamadaService _service;

        public ChamadaController(IChamadaService service)
        {
            _service = service;
        }

        [HttpGet("Aula/{aulaId:int}")]
        public async Task<IActionResult> GetByAulaId(int aulaId)
        {
            var chamadas = await _service.GetByAulaIdAsync(aulaId);
            return Ok(chamadas);
        }

        [HttpGet("Aluno/{alunoId:int}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            var chamadas = await _service.GetByAlunoIdAsync(alunoId);
            return Ok(chamadas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Chamada chamada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdChamada = await _service.AddAsync(chamada);
            return CreatedAtAction(nameof(GetByAulaId), new { aulaId = chamada.AulaId }, createdChamada);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Chamada chamada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, chamada);
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
