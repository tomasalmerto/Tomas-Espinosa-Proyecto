using Heladeria.Data.Entidades;
using Heladeria.Data.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heladeria.Web.Controllers
{
    [Authorize(Roles = "Admin,Usuario")]
    public class SaborController : Controller
    {
        private readonly ISaborRepositorio _saborRepositorio;

        public SaborController(ISaborRepositorio saborRepositorio)
        {
            _saborRepositorio = saborRepositorio;
        }

        public async Task<IActionResult> Index()
        {
            bool esAdmin = User.IsInRole("Admin");

            var listaSabores = await _saborRepositorio.ObtenerTodosAsync(incluirEliminadosLogicos: esAdmin);
            return View(listaSabores);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear()
        {
            var categorias = await _saborRepositorio.ObtenerCategoriasAsync();
            ViewBag.Categorias = categorias;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear(Heladeria.Data.Entidades.Sabor nuevoSabor)
        {
            if (ModelState.IsValid)
            {
                await _saborRepositorio.AgregarAsync(nuevoSabor);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = await _saborRepositorio.ObtenerCategoriasAsync();
            return View(nuevoSabor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BorrarLogico(int id)
        {
            var sabor = await _saborRepositorio.ObtenerPorIdAsync(id);

            if (sabor != null)
            {
                sabor.Disponible = false; // Lo ocultamos para el usuario común
                await _saborRepositorio.ActualizarAsync(sabor);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Restaurar(int id)
        {
            var sabor = await _saborRepositorio.ObtenerPorIdAsync(id);

            if (sabor != null)
            {
                sabor.Disponible = true;
                await _saborRepositorio.ActualizarAsync(sabor);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BorrarFisico(int id)
        {
            await _saborRepositorio.BorrarFisicoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id)
        {
            var sabor = await _saborRepositorio.ObtenerPorIdAsync(id);
            if (sabor == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = await _saborRepositorio.ObtenerCategoriasAsync();
            return View(sabor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id, Heladeria.Data.Entidades.Sabor saborModificado)
        {
            if (id != saborModificado.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _saborRepositorio.ActualizarAsync(saborModificado);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = await _saborRepositorio.ObtenerCategoriasAsync();
            return View(saborModificado);
        }
    }
}

