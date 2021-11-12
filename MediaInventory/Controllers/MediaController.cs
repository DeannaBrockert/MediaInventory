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
    }
}
