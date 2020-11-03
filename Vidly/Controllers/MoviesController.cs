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
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Patalok"
            };

            var customers = new List<Customer>()
            {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };

            var VM = new RandomCustomerMovieViewModel()
            {
                Customers = customers,
                Movie = movie
            };

            return View(VM);
        }
    }
}