using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInventory.Models;
using Microsoft.EntityFrameworkCore;

/**************************************************
 * Date         Name            Comments
 * 11/5/21      Deanna B        First deployment of mediahasborrower controller. Creating of checkout views
 * 11/19/21     Deanna B        Add code for add, update, and delete.
 * 12/3/21      Deanna B        Changed functions to use stored procedures.
 * 
 * ************************************************/

namespace MediaInventory.Controllers
{
    public class MediaHasBorrowerController : Controller
    {
        private mediaInventoryDBrockertContext context { get; set; }
        public MediaHasBorrowerController(mediaInventoryDBrockertContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var mediahasborrowers = context.MediaHasBorrowers.Include(m => m.Media).OrderBy(m => m.Media.MediaName).Include(b => b.Borrower).ToList();
            return View(mediahasborrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.LastName).ToList();
            ViewBag.Media = context.Media.OrderBy(m => m.MediaName).ToList();
            MediaHasBorrower newcheckout = new MediaHasBorrower();
            newcheckout.BorrowDate = DateTime.Today;
            return View("Edit", newcheckout);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.LastName).ToList();
            ViewBag.Media = context.Media.OrderBy(d => d.MediaName).ToList();
            var mediahasborrower = context.MediaHasBorrowers.Find(id);
            return View(mediahasborrower);
        }

        [HttpPost]
        public IActionResult Edit(MediaHasBorrower mediahasborrower)
        {
            if (ModelState.IsValid)
            {
                string returnedDate = mediahasborrower.ReturnDate.ToString();
                returnedDate = (returnedDate == "") ? null : mediahasborrower.ReturnDate.ToString();
                if (mediahasborrower.MediaHasBorrowerId == 0)
                {
                    //context.MediaHasBorrowers.Add(mediahasborrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_media_has_borrower @p0, @p1, @p2, @p3", parameters: new[] { mediahasborrower.MediaId.ToString(), mediahasborrower.BorrowerId.ToString(), mediahasborrower.BorrowDate.ToString(), returnedDate });
                }
                else
                {
                    //context.MediaHasBorrowers.Update(mediahasborrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_media_has_borrower @p0, @p1, @p2, @p3, @p4", parameters: new[] { mediahasborrower.MediaHasBorrowerId.ToString(), mediahasborrower.MediaId.ToString(), mediahasborrower.BorrowerId.ToString(), mediahasborrower.BorrowDate.ToString(), returnedDate });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "MediaHasBorrower");
            }
            else
            {
                ViewBag.Action = (mediahasborrower.MediaHasBorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.LastName).ToList();
                ViewBag.Media = context.Media.OrderBy(d => d.MediaName).ToList();
                return View(mediahasborrower);
            }
        }
    }
}
