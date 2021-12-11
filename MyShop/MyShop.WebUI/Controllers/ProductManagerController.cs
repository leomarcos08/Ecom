using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult CreateProduct()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult CreateProduct(Product _product)
        {
            if (ModelState.IsValid)
            {
                return View(_product);
            }
            else
            {
                context.Insert(_product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult UpdateProduct(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
                return View(product);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product _product, string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    return View(product);
                }

                product.Category = _product.Category;
                product.Description = _product.Description;
                product.Image = _product.Image;
                product.Name = _product.Name;
                product.Price = _product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
                
        }

        public ActionResult DeleteProduct(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
                return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Product _product, string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}