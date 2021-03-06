﻿using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        Context ctx=new Context();
        public ActionResult ProductsTable()
        {
            
            return View(ctx.Products);

        }
        [HttpGet]
        public ActionResult DetailTable()
        {

            var products = ctx.Products.Include("Descriptions").Include("Properties").ToList();
            List<DetailTableModel> mymodels = new List<DetailTableModel>();
            foreach(var el in products)
            {
                var model = new DetailTableModel();
                model.Product = el;
                model.Descriptions = el.Descriptions.ToList();
                model.Properties = el.Properties.ToList();
                mymodels.Add(model);
            }
            
           
            return View(mymodels);

        }
        public ActionResult LinkPage(string id)
        {
            if (ctx.Products.Any(x=>x.Name==id))
                return Redirect(ctx.Products.Where(v => v.Name == id).First().Link);
            else
                return HttpNotFound();

        }
       
        public ActionResult LinkDescription(string id)
        {
            //var desc = ctx.Descriptions.Where(v => v.IdProduct == ctx.Products.Where(c => c.Name == id).FirstOrDefault().Id).FirstOrDefault();


           // if (desc != null)
             //   return Json(desc, JsonRequestBehavior.AllowGet);
            //else
                return Json(new  { Text= "Not found" }, JsonRequestBehavior.AllowGet); 

        }
   

    }
}