using Mongo3.App_Start;
using Mongo3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mongo3.Controllers
{
    public class TratamientosController : Controller
    {
        private MongoDBContext dbcontext;
        private MongoDB.Driver.IMongoCollection<TratamientoModel> TratamientoCollection;

        // GET: Tratamientos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tratamientos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tratamientos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tratamientos/Create
        [HttpPost]
        public ActionResult Create(TratamientoModel tratamiento)
        {
            try
            {
                
                TratamientoCollection.InsertOne(tratamiento);

                return RedirectToAction("Index");

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Tratamientos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tratamientos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tratamientos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tratamientos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
