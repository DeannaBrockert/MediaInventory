using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInventory.Models;

namespace MediaInventory.Controllers
{
    public class MediaController : Controller
    {
        private mediaInventoryDBrockertContext context { get; set; }
        public MediaController(mediaInventoryDBrockertContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            List<Medium> media = context.Media.OrderBy(a => a.MediaName).ToList();
            return View(media);
        }
        [HttpGet]
        public IActionResult Add() 
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
            Medium newmedia = new Medium();
            newmedia.ReleaseDate = DateTime.Today;
            return View("Edit", new Medium());
        }

            [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            var media = context.Media.Find(id);
            return View(media);
        }
        [HttpPost]
        public IActionResult Edit(Medium media)
        {
            if (ModelState.IsValid)
            {
                if (media.MediaId == 0)
                {
                    context.Media.Add(media);
                }
                else
                {
                    context.Media.Update(media);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Media");
            }
            else
            {
                ViewBag.Action = (media.MediaId == 0) ? "Add" : "Edit";
                ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.DiskTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
                return View(media);
            }

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var media = context.Media.Find(id);
            return View(media);
        }

        [HttpPost]
        public IActionResult Delete(Medium media)
        {
            context.Media.Remove(media);
            context.SaveChanges();
            return RedirectToAction("Index", "Media");
        }
    }
}
