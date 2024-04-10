using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace FinalProject.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmRepository _repo;

        public FilmController(IFilmRepository repo)
        {
            _repo = repo;
        }



        public IActionResult UploadButtonClick(IFormFile files, Film film)
        {
            if (files == null || files.Length == 0 || film == null)
            {
              
                return RedirectToAction("Index");
            }

            try
            {
                var fileName = Path.GetFileName(files.FileName);
                var fileExtension = Path.GetExtension(fileName);

                
                if (!IsImageFile(fileExtension))
                {
                   
                    return RedirectToAction("Index");
                }

                var uniqueFileName = Guid.NewGuid().ToString();
                var newFileName = $"{uniqueFileName}{fileExtension}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newFileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }

                film.Image = "/images/" + newFileName;
                _repo.InsertImage(film);
            }
            catch (Exception ex)
            {
               
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        private bool IsImageFile(string fileExtension)
        {
            // Add more extensions if needed
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            return allowedExtensions.Contains(fileExtension.ToLower());
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
