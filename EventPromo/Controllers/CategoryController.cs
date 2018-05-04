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
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            List<EventCategory> categories = context.Categories.ToList();
            return View(categories);
        }

        private readonly EventDbContext context;

        public CategoryController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                // Adds a new Cheese category
                EventCategory newCategory = new EventCategory

                { Name = addCategoryViewModel.Name, };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/Category");
            }
            return View(addCategoryViewModel);
        }
    }
}
