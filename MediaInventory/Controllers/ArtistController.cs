using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInventory.Models;
using Microsoft.EntityFrameworkCore;

/**************************************************
 * Date         Name            Comments
 * 11/5/21      Deanna B        First deployment of artist controller. Creating of artist views
 * 11/19/21     Deanna B        Add code for add, update, and delete.
 * 12/3/21      Deanna B        Changed functions to use stored procedures.
 * 
 * ************************************************/

namespace MediaInventory.Controllers
{
    public class ArtistController : Controller
    {
        private mediaInventoryDBrockertContext context { get; set; }
        public ArtistController(mediaInventoryDBrockertContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var artists = context.Artists.OrderBy(a => a.ArtistName).ToList();
            return View(artists);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            return View("Edit", new Artist());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (artist.ArtistId == 0)
                {
                    //context.Artists.Add(artist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1", parameters: new[] { artist.ArtistName, artist.ArtistTypeId.ToString() });
                    
                }
                else
                {
                    //context.Artists.Update(artist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2", parameters: new[] { artist.ArtistId.ToString(), artist.ArtistName, artist.ArtistTypeId.ToString() });
                }
               
                //context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            } 
            else
            {
                ViewBag.Action = (artist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
                return View(artist);
            }

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0", parameters: new[] { artist.ArtistId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}
