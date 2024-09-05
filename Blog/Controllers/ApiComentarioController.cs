using Blog.Models;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiComentarioController : ControllerBase
    {
        private readonly BlogContext _context;
        // GET: api/<ApiComentarioController>
        public ApiComentarioController(BlogContext context) { 
            _context = context;
        }

        [HttpGet]
        public async Task<List<Comentario>> Get() => await _context.Comentarios.ToListAsync();

        // GET api/<ApiComentarioController>/5
        [HttpGet("{id}")]
        public async Task<Comentario> Get(int id)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            return comentario;
        }

        // POST api/<ApiComentarioController>
        [HttpPost]
        public async Task<Comentario> Post([FromBody] Comentario com)
        {
            var comentario = new Comentario() { 
                Contenido = com.Contenido,
                Fecha = DateTime.Now,
                IdUsuario = com.IdUsuario,
            };
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            
            return comentario; 
        }

        // PUT api/<ApiComentarioController>/5
        [HttpPut("{id}")]
        public async Task<Comentario> Put(int id, [FromBody] Comentario com)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(m => m.Id == id);
            comentario.Contenido = com.Contenido;
            comentario.Fecha = DateTime.Now;
            await _context.SaveChangesAsync();
            return comentario;
        }

        // DELETE api/<ApiComentarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comentario = await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            if (comentario == null) {
                return NotFound();
            }
            _context.Comentarios.Remove(comentario);

            await _context.SaveChangesAsync();
            return Ok(comentario);
        }
    }
}
