using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmRepository _repo;

        public FilmController(IFilmRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var films = _repo.GetAllFilms();
            return View(films);
        }

        public IActionResult ViewFilm(int id)
        {
            var film = _repo.GetFilm(id);
            return View(film);
        }

        public IActionResult UpdateFilm(int id)
        {
            Film film = _repo.GetFilm(id);
            if (film == null)
            {
                return View("FilmNotFound");
            }
            return View(film);
        }

        public IActionResult UpdateFilmToDatabase(Film film)
        {
            _repo.UpdateFilm(film);

            return RedirectToAction("ViewFilm", new { id = film.ID });
        }
        public IActionResult InsertFilm()
        {
            var film = _repo.AssignCategory();
            return View(film);
        }

        public IActionResult InsertFilmToDatabase(Film filmToInsert)
        {
            _repo.InsertFilm(filmToInsert);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFilm(Film film)
        {
            _repo.DeleteFilm(film);
            return RedirectToAction("Index");
        }
    }
}
