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
            if (files.Length != null)
            {
                if (files.Length > 0)
                {


                    var fileName = Path.GetFileName(files.FileName);

                    var uniqueFileName = Convert.ToString(Guid.NewGuid());

                    var fileExtension = Path.GetExtension(fileName);

                    var newFileName = String.Concat(uniqueFileName, fileExtension);

                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image")).Root + $@"{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }

                    film.Image = "/images/" + newFileName;

                    _repo.InsertImage(film);

                }

            }

            return RedirectToAction("Index");
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
