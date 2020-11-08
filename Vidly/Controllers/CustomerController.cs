using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    _context.Dispose();
        //}
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(m =>m.MembershipType).ToList();
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NewCustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                var vM = new NewCustomerViewModel
                {
                    Customers = customer.Customers,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("New",vM);
            }

            if (customer.Customers.Id == 0)
            {
                _context.Customers.Add(customer.Customers);
            }
            else
            {
                var customerInDb = _context.Customers.Single(m => m.Id == customer.Customers.Id);

                customerInDb.Name = customer.Customers.Name;
                customerInDb.Birthdate = customer.Customers.Birthdate;
                customerInDb.IsSubscribeToNewsletter = customer.Customers.IsSubscribeToNewsletter;
                customerInDb.MembershipTypeId = customer.Customers.MembershipTypeId;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index","Customer");
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(m=>m.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vM = new NewCustomerViewModel
                {
                    Customers = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("New", vM);
            }
        }

        public ActionResult New()
        {
            var mT = _context.MembershipTypes.ToList();
            var vM = new NewCustomerViewModel
            {
                Customers = new Customer(),
                MembershipTypes = mT
            };
            return View(vM);
        }

    }
}