using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        /* IActionResult : Index Action Method
         */
        public IActionResult Index()
        {
            /* Task
             * We want to display all the categories as a list in HTML,
             * for that I have to first receive all the data from the DB in Index()
             * 
             * Then pass the data to the view
             */
            
            // This will get the categories from the DB
            IEnumerable<Category> objCategoryList = _db.Categories;

            return View(objCategoryList);
        }

        //Get action
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            // if the model is not valid, i.e. either of the given field is empty
            // or not filled by the user
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                // It will be used for a notification on the page
                TempData["success"] = "Category Created Successfully";

                // after saving data back in the DB, redirected to 'Index'
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get action
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            // if the model is not valid, i.e. either of the given field is empty
            // or not filled by the user
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();

                TempData["success"] = "Category Edited Successfully";

                // after saving data back in the DB, redirected to 'Index'
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get action
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Category Deleted Successfully";

            return RedirectToAction("Index");
        }
    }
}
