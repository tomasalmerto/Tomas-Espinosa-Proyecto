using Microsoft.AspNetCore.Mvc;
using Heladeria.Data.Repositorios;

namespace Heladeria.Web.Controllers
{
    public class SaborController : Controller
    {
        private readonly ISaborRepositorio _saborRepositorio;

        
        public SaborController(ISaborRepositorio saborRepositorio)
        {
            _saborRepositorio = saborRepositorio;
        }

        
        public async Task<IActionResult> Index()
        {
            var listaSabores = await _saborRepositorio.ObtenerTodosAsync();
            return View(listaSabores);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var categorias = await _saborRepositorio.ObtenerCategoriasAsync();
            ViewBag.Categorias = categorias;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> BorrarLogico(int id)
        {
            
            var sabor = await _saborRepositorio.ObtenerPorIdAsync(id);

            if (sabor != null)
            {
                
                sabor.Disponible = false;

                
                await _saborRepositorio.ActualizarAsync(sabor);
            }

            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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

