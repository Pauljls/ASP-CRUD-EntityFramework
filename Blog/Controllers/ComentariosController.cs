using Blog.Models;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Blog.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly BlogContext _context;

        public ComentariosController(BlogContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var comentarios = _context.Comentarios.Include(c => c.IdUsuarioNavigation);
            return View(await comentarios.ToListAsync());
        }

        public IActionResult create()
        {
            ViewData["usuarios"] = new SelectList(_context.Usuarios,"Id","Nombre") ;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComentarioViewModel model)
        {
            Console.WriteLine(JsonSerializer.Serialize(model));
            // Validamos que el modelo recibido sea válido
            if (ModelState.IsValid)
            {
                // Buscar el usuario en la base de datos por el nombre que llega en el ViewModel
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == int.Parse(model.Usuario));


                // Validamos que el usuario exista
                if (usuario == null)
                {
                    // Si no se encuentra el usuario, agregamos un error al ModelState
                    ModelState.AddModelError("Usuario", "Usuario no encontrado");
                    ViewData["usuarios"] = new SelectList(_context.Usuarios, "Id", "Nombre");
                    return View(model);
                }

                // Crear el objeto Comentario con los datos del ViewModel y el id del usuario encontrado

                
                var comentario = new Comentario
                {
                    Contenido = model.Comentario,
                    Fecha = DateTime.Now,  // Asignamos la fecha actual
                    IdUsuario = usuario.Id  // Usamos el id del usuario encontrado
                };

                // Guardamos el comentario en la base de datos
                _context.Comentarios.Add(comentario);
                await _context.SaveChangesAsync();

                // Redirigir al índice después de guardar
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, volvemos a cargar la lista de usuarios
            ViewData["usuarios"] = new SelectList(_context.Usuarios, "id", "nombre");
            return View(model);
        }

    }
}
