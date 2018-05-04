using Microsoft.AspNetCore.Mvc;
//using EventMVC.Models;
using System.Collections.Generic;
//using EventMVC.ViewModels;
//using EventMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EventPromo.Controllers
{
    public class EventController : Controller
    {
        private readonly EventDbContext context;

        public EventController(EventDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Event> Event = context.Event.Include(c => c.Category).ToList();

            return View(event);
        }

        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel(context.Categories.ToList());
            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                EventCategory newEventCategory = context.Categories.Single(c => c.ID == addEventViewModel.CategoryID);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    Category = newEventCategory
                    //CategoryID = addEventViewModel.CategoryID,
                };

                context.Event.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Event");
            }

            return View(addEventViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Event";
            ViewBag.cheeses = context.Event.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] EventIds)
        {
            foreach (int cheeseId in EventIds)
            {
                Event theCheese = context.Event.Single(c => c.ID == EventId);
                context.Event.Remove(theEvent);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        //in the video
        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                return Redirect("/Category");
            }

            EventCategory theCategory = context.Categories
                .Include(cat => cat.Event)
                .Single(cat => cat.ID == id);

            ViewBag.title = "Event in category: " + theCategory.Name;

            return View("Index", theCategory.Event);
        }
    }
}