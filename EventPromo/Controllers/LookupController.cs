using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using EventMVC.Data;
//using EventMVC.Models;
//using EventMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EventPromo.Controllers
{
    public class LookupController : Controller
    {
        private EventDbContext context;

        public MenuController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Lookup> menus = context.Menus.ToList();
            //            IList<Lookup> lookup = context.Menus.ToList();
            return View(lookup);
        }

        public IActionResult Add()
        {
            AddLookupViewModel addMenuViewModel = new AddLookupViewModel();
            return View(addLookupViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddLookupViewModel addLookupViewModel)
        {
            if (ModelState.IsValid)
            {
                Lookup newLookup = new Lookup
                {
                    Name = addLookupViewModel.Name
                };

                context.Lookup.Add(newLookup);
                context.SaveChanges();

                return Redirect("/Menu/Lookup/" + newLookup.ID);
            }
            return View(addLookupViewModel);
        }

        public IActionResult LookupMenu(int id)
        {
            Lookup lookup = context.Lookup.Single(m => m.ID == id);
            List<Lookup> items = context
                .Lookup
                .Include(item => item.Event)
                .Where(cm => cm.LookupID == id)
                .ToList();

            ViewMenuViewModel viewLookup = new ViewMenuViewModel
            {
                Lookup = lookup,
                Items = items
            };

            return View(viewLookup);
        }

        public IActionResult AddItem(int id)
        {
            Lookup lookup = context.Menus.Single(m => m.ID == id);
            List<Event> Event = context.Event.ToList();

            AddLookupItemViewModel viewMenu = new AddLookupItemViewModel(lookup, event);
            return View(viewLookup);
        }

        [HttpPost]
        public IActionResult AddItem(AddLookupItemViewModel addLookupItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var eventID = addLookupItemViewModel.EventID;
                var lookupID = addLookupItemViewModel.MenuID;
                var lookupID = addLookupItemViewModel.MenuID;

                IList<CheeseMenu> existingItems = context.Lookup
                    .Where(cm => cm.PromoID == eventID)
                    .Where(cm => cm.LookupID == LookupID).ToList();

                if (existingItems.Count == 0)
                {
                    Lookup lookupItem = new Lookup
                    {
                        Event = context.Event.Single(c => c.ID == eventID),
                        Lookup = context.Lookup.Single(m => m.ID == lookupID)
                    };

                    context.Lookup.Add(lookupItem);
                    context.SaveChanges();
                }
                return Redirect("/Event/Lookup/" + addLookupItemViewModel.LookupID);
            }
            return View(addLookupItemViewModel);
        }
    }
}
