using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInventory.Models;

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
            List<Borrower> borrowers = context.Borrowers.OrderBy(a => a.FirstName).ThenBy(a => a.LastName).ToList();
            return View(borrowers);
        }
    }
}
