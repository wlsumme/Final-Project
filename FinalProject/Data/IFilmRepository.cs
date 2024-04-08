using System;
using System.Collections.Generic;
using FinalProject.Models;

namespace FinalProject.Data
{
    public interface IFilmRepository
    {
        public IEnumerable<Film> GetAllFilms();
        public Film GetFilm(int id);

        public void UpdateFilm(Film film);

        public void InsertFilm(Film filmToInsert);

        public IEnumerable<Category> GetCategories();

        public Film AssignCategory();

        public void DeleteFilm(Film film);



    }
}
