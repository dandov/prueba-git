using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcMovie.Service
{
    public class MovieService
    {
        private static MovieDBContext db = new MovieDBContext();

        public static List<Movie> Index()
        {
            return db.Movies.ToList();
        }

        public static Movie Find(int id)
        {
            return db.Movies.Find(id);
        }

        public static void CrearMovie(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public static void EditMovie(Movie movie)
        {
            Movie m = Find(movie.ID);
            CopyMovie(m, movie);
            db.SaveChanges();
        }

        private static void CopyMovie(Movie to, Movie from)
        {
            if (to!=null && from!=null)
            {
                to.Price = from.Price;
                to.Rating = from.Rating;
                to.ReleaseDate = from.ReleaseDate;
                to.Title = from.Title;
                to.Genre = from.Genre;
            }
        }

        public static void DeleteMovie(Movie movie)
        {
            db.Movies.Remove(movie);
            db.SaveChanges();
        }

        public static List<string> GetGendres()
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());

            return GenreLst;
        }

        public static IQueryable<Movie> SearchMovie(string movieGenre, string searchString)
        {
            IQueryable<Movie> movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return movies;
        }
    }
}