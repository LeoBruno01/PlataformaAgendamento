using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAgendamento.Domain.Entities;
using PlataformaAgendamento.Infrastructure;
using PlataformaAgendamento.Infrastructure.Data;

namespace PlataformaAgendamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Cadastra um novo serviço
        [HttpPost]
        public async Task<IActionResult> CriarServico([FromBody] Servico nuevoServico)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Servicos.Add(nuevoServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = nuevoServico.Id }, nuevoServico);
        }

        // GET: Lista todos os serviços
        [HttpGet]
        public async Task<IActionResult> ListarServicos()
        {
            var servicos = await _context.Servicos.ToListAsync();
            return Ok(servicos);
        }

        // GET por ID: Busca um serviço específico
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
                return NotFound("Serviço não encontrado.");

            return Ok(servico);
        }

        // Atualiza um serviço existente (ex: mudar preço)
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarServico(int id, [FromBody] Servico servicoAtualizado)
        {
            if (id != servicoAtualizado.Id)
                return BadRequest("O ID do caminho não corresponde ao ID do objeto.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Entry(servicoAtualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Servicos.Any(e => e.Id == id))
                    return NotFound("Serviço não encontrado.");
                else
                    throw;
            }

            return NoContent(); // 204: Alterado com sucesso, sem corpo no retorno
        }

        // Remove um serviço do cardápio da barbearia
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirServico(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
                return NotFound("Serviço não encontrado.");

            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = $"O serviço '{servico.Nome}' foi excluído com sucesso!" });
        }
    }
}