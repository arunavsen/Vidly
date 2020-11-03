using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        private List<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie{Id = 1,Name = "Mirzapur"},
                new Movie{Id = 2,Name = "Patalok"}
            };
        }
    }
}