using Microsoft.AspNetCore.Mvc;
using SegundaPracticaDDB.Models;
using SegundaPracticaDDB.Repositories;

namespace SegundaPracticaDDB.Controllers {
    public class ComicController : Controller {

        IRepositoryComic repo;

        public ComicController(IRepositoryComic repo) {
            this.repo = repo;
        }

        public IActionResult Index() {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Comic comic) {
            this.repo.CreateComic(comic.Nombre, comic.Imagen, comic.Descripcion);
            return RedirectToAction("Index");
        }
    }
}
