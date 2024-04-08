using Dapper;
using FinalProject.Models;
using System.Data;

namespace FinalProject.Data
{
    public class FilmRepository : IFilmRepository
    {
        private readonly IDbConnection _conn;

        public FilmRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Film> GetAllFilms()
        {
            return _conn.Query<Film>("SELECT * FROM films");
        }

        public Film GetFilm(int id)
        {
            return _conn.QuerySingle<Film>("SELECT * FROM FILMS WHERE ID = @id", new { id = id });
        }

        public void UpdateFilm(Film film)
        {
            _conn.Execute(@"UPDATE films 
                    SET Title = @Title, 
                        Director = @Director,
                        Writer = @Writer,
                        Composer = @Composer, 
                        RTS = @RTS, 
                        Runtime = @Runtime, 
                        Pee_Time = @Pee_Time, 
                        Checked = @Checked 
                    WHERE ID = @ID",
                    new
                    {
                        Title = film.Title,
                        Director = film.Director,
                        Writer = film.Writer,
                        Composer = film.Composer,
                        RTS = film.RTS,
                        Runtime = film.Runtime,
                        Pee_Time = film.Pee_Time,
                        Checked = film.Checked,
                        ID = film.ID

                    });
        }

        public void InsertFilm(Film filmToInsert)
        {
            _conn.Execute(@"INSERT INTO films 
                    (ID, TITLE, DIRECTOR, WRITER, COMPOSER, RTS, RUNTIME, PEE_TIME, CHECKED) 
                    VALUES 
                    (@id, @title, @director, @writer, @composer, @rts, @runtime, @pee_time, @checked);",
        new
        {
            Id = filmToInsert.ID,
            Title = filmToInsert.Title,
            Director = filmToInsert.Director,
            Writer = filmToInsert.Writer,
            Composer = filmToInsert.Composer,
            RTS = filmToInsert.RTS,
            Runtime = filmToInsert.Runtime,
            Pee_Time = filmToInsert.Pee_Time,
            Checked = filmToInsert.Checked
        });
        }

        public IEnumerable<Category> GetCategories()
        {
            return _conn.Query<Category>("SELECT * FROM categories;");
        }

        public Film AssignCategory()
        {
            var categoryList = GetCategories();
            var film = new Film();
            film.Categories = categoryList;
            return film;
        }

        public void DeleteFilm(Film film)
        {
            _conn.Execute("DELETE FROM FILMS WHERE ID = @id;", new { id = film.ID });
            
        }
    }
}
