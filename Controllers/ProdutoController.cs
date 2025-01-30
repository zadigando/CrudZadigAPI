using Microsoft.AspNetCore.Mvc;
using CrudZadigAPI.Data;
using CrudZadigAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudZadigAPI.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos.ToListAsync();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} nao encontrado");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Dados do produto inválidos");
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                return BadRequest("O nome do produto é obrigatorio");
            }

            if (produto.Preco <= 0)
            {
                return BadRequest("O preço do produto deve ser maior que zero");
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Produto produtoAtualizado)
        {
            if (id != produtoAtualizado.Id)
            {
                return BadRequest("IDs nao correspondem");
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado");
            }

            produto.Nome = produtoAtualizado.Nome;
            produto.Descricao = produtoAtualizado.Descricao;
            produto.Preco = produtoAtualizado.Preco;
            produto.Quantidade = produtoAtualizado.Quantidade;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} nao encontrado");
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
