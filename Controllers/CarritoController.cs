using Microsoft.AspNetCore.Mvc;
using FGC_PWA2025.Models;
using FGC_PWA2025.Helpers;
using System.Linq;


namespace FGC_PWA2025.Controllers
{
    public class CarritoController : Controller
    {
        private readonly PeliculasDbContext _DbContext;
        public List<Pelicula> Peliculas = new List<Pelicula>();
        public List<Item> Mycart = new List<Item>();
        public decimal total = 0;

        int contar = 0;
        public CarritoController(PeliculasDbContext context)
        {
            _DbContext = context;
        }

        public IActionResult Carrito(string titulo, string genero, int? fecha)
        {
            int cantidad;

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null)
            {
                cantidad = 0;
            }
            else
            {
                cantidad = cart.Count();
            }

            TempData["Contar"] = cantidad;

            var peliculas = _DbContext.Peliculas.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
            {
                peliculas = peliculas.Where(p => p.Titulo.Contains(titulo));
            }

            if (!string.IsNullOrEmpty(genero))
            {
                peliculas = peliculas.Where(p => p.Genero.Contains(genero));
            }

            if (fecha.HasValue)
            {
                peliculas = peliculas.Where(p => p.Fecha == fecha.Value);
            }

            ViewData["Titulo"] = titulo;
            ViewData["Genero"] = genero;
            ViewData["Fecha"] = fecha;

            return View(peliculas.ToList());
        }
        public IActionResult Comprar()
        {
            var MyCart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (MyCart == null)
            {
                return RedirectToAction("Carrito");
            }
            else
            {
                return View(MyCart);
            }
        }
        public int ContarItems(List<Item> items)
        {
            int cantidad = items.Count();
            return cantidad;
        }
        private int Exists(List<Item> cart, string id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Pelicula.ID.ToString().Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [HttpGet]
        public IActionResult Cart(string id)
        {
            var pelicula = _DbContext.Peliculas.Find(int.Parse(id));  // ID es int, parseamos

            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null)
            {
                cart = new List<Item>();
                cart.Add(new Item()
                {
                    Pelicula = pelicula,
                    Cantidad = 1
                });

                TempData["Contar"] = ContarItems(cart);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exists(cart, id);

                if (index == -1)
                {
                    cart.Add(new Item()
                    {
                        Pelicula = pelicula,
                        Cantidad = 1
                    });
                }
                else
                {
                    var newCantidad = cart[index].Cantidad + 1;
                    cart[index].Cantidad = newCantidad;
                }

                TempData["Contar"] = ContarItems(cart);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Carrito");
        }
        [HttpGet]
        public IActionResult Quitar(string id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            int index = Exists(cart, id);

            if (index != -1)
            {
                cart.RemoveAt(index);
            }

            TempData["Contar"] = ContarItems(cart);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Carrito");
        }
        [HttpGet]
        public IActionResult Vaciar()
        {
            HttpContext.Session.Remove("cart");  // borra el carrito
            TempData["Contar"] = 0;
            return RedirectToAction("Carrito");
        }



    }
}
