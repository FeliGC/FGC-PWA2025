using FGC_PWA2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace FGC_PWA2025.Controllers
{
    public class EditarController : Controller
    {

        private readonly PeliculasDbContext _DbContext;

        public EditarController(PeliculasDbContext _context)
        {
            _DbContext = _context;
        }
        public IActionResult Editar()
        {       
            List<Pelicula> p = _DbContext.Peliculas.ToList();   

            return View(p);
        }
        [HttpGet]
        public IActionResult Detalle(int ID)
        {
            Pelicula _Pelicula = new Pelicula();

            if (ID != 0)
            {
                _Pelicula = _DbContext.Peliculas.Find(ID);
            }
            return View(_Pelicula);
        }

        [HttpPost]
        public IActionResult Detalle(Pelicula modelPelicula)
        {
            if (!ModelState.IsValid)
            {
                
                return View(modelPelicula);
            }

            if (modelPelicula.ID == 0)
            {
                _DbContext.Peliculas.Add(modelPelicula);
            }
            else
            {
                _DbContext.Peliculas.Update(modelPelicula);
            }
            _DbContext.SaveChanges();

            return RedirectToAction("Editar", "Editar");
        }

        [HttpGet]
        public IActionResult Eliminar(int ID)
        {
            Pelicula _Pelicula = new Pelicula();

            if (ID != 0)
            {
                _Pelicula = _DbContext.Peliculas.Find(ID);
            }
            return View(_Pelicula);
        }

        [HttpPost]
        public IActionResult Eliminar(Pelicula modelPelicula)
        {
            _DbContext.Peliculas.Remove(modelPelicula);
            _DbContext.SaveChanges();

            return RedirectToAction("Editar", "Editar");
        }

    }
}
