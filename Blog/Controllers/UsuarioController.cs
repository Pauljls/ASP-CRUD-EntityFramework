using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BlogContext _context;

        public UsuarioController(BlogContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _context.Usuarios.ToListAsync());
        }
    }
}
