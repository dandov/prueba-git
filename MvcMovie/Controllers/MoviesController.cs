using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using MvcMovie.Service;

namespace MvcMovie.Controllers
{ 
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        //
        // GET: /Movies/

        public ViewResult Index()
        {
            return View(MovieService.Index());
        }

        //
        // GET: /Movies/Details/5

        public ViewResult Details(int id)
        {
            return View(MovieService.Find(id));
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovieService.CrearMovie(movie);
                return RedirectToAction("Index");  
            }

            return View(movie);
        }
        
        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Movie movie = MovieService.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovieService.EditMovie(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: /Movies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Movie movie = MovieService.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Movie movie = MovieService.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            MovieService.DeleteMovie(movie);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SearchIndex(string movieGenre, string searchString)
        {
            ViewBag.movieGenre = new SelectList(MovieService.GetGendres());
            return View(MovieService.SearchMovie(movieGenre, searchString));
        }
    }
}