using FGC_PWA2025.Models;
using FGC_PWA2025.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace FGC_PWA2025.Controllers
{
    public class MenuController : Controller
    {
        private readonly PeliculasDbContext _DbContext;

        public MenuController(PeliculasDbContext context)
        {
            _DbContext = context;
        }

        public IActionResult Menu(int pg = 1)
        {
            const int pageSize = 5;

            if (pg < 1)
                pg = 1;

            var lista = _DbContext.Peliculas.OrderBy(p => p.Titulo).ToList();
            int recsCount = lista.Count();

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var data = lista.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            return View(data);
        }
    }
}
