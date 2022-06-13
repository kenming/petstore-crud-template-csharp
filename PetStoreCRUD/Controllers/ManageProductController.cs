using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thinksoft.crudTutorial.EDM;
using Thinksoft.crudTutorial.Services;

namespace Thinksoft.crudTutorial.Controllers
{    
    public class ManageProductController : Controller
    {
        private readonly ManageProductService? Service = default!;

        public ManageProductController()
        {
            Service = new ManageProductService();
        }

        // GET: ManageProductController
        public IActionResult Index()
        {            
            var Model = Service.ListAllProduct();

            return View(Model);
        }

        // GET: ManageProductController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product aProduct)
        {
            int Id = Service.Add(aProduct);

            ViewBag.Message = "Product Id=" + Id + " added successfully.";

            return View();
        }

        // GET: ManageProductController/Edit/5
        public IActionResult Edit(int productId)
        {
            Product aProduct = Service.Get(productId);

            return View(aProduct);
        }

        // POST: ManageProductController/Edit/5
        [HttpPost]        
        public IActionResult Edit(Product aProduct)
        {
            int RowCount = Service.Update(aProduct);
            string Message = "Product ID:" + aProduct.Id;

            if (RowCount == 1)
                Message += " updated successfully.";
            else
                Message += " updated failure.";

            ViewBag.Message = Message;

            return View(aProduct);
        }

        // GET: ManageProductController/Delete/5
        public IActionResult Delete(int productId)
        {
            int RowCount = Service.Delete(productId);
            string Message = "Product ID:" + productId;

            if (RowCount == 1)
                Message += " deleted successfully.";
            else
                Message += " deleted failure.";

            ViewBag.Message = Message;

            return RedirectToAction("Index");
        }

    }
}
