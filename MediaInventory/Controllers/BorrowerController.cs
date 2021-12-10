using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInventory.Models;
using Microsoft.EntityFrameworkCore;

/**************************************************
 * Date         Name            Comments
 * 11/5/21      Deanna B        First deployment of borrower controller. Creating of borrower views
 * 11/19/21     Deanna B        Add code for add, update, and delete.
 * 12/3/21      Deanna B        Changed functions to use stored procedures.
 * 
 * ************************************************/

namespace MediaInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private mediaInventoryDBrockertContext context { get; set; }
        public BorrowerController(mediaInventoryDBrockertContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Borrower> borrowers = context.Borrowers.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
            return View(borrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                {
                    //context.Borrowers.Add(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_borrower @p0, @p1, @p2", parameters: new[] { borrower.FirstName, borrower.LastName, borrower.PhoneNumber.ToString() });
                }
                else
                {
                    //context.Borrowers.Update(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_borrower @p0, @p1, @p2, @p3", parameters: new[] { borrower.BorrowerId.ToString(), borrower.FirstName, borrower.LastName, borrower.PhoneNumber.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }
        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            //context.Borrowers.Remove(borrower);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_borrower @p0", parameters: new[] { borrower.BorrowerId.ToString() });
            return RedirectToAction("Index", "Borrower");
        }
    }
}
