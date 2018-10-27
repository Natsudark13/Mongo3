using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using Mongo3.App_Start;
using Mongo3.Models;

namespace Mongo3.Controllers
{
    public class CentroAtencionController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<CentroAtencionModel> CentroAtencionCollection;

        public CentroAtencionController()
        {
            dbcontext = new MongoDBContext();
            CentroAtencionCollection = dbcontext.database.GetCollection<CentroAtencionModel>("CentroAtencion");
        }

        // GET: Funcionarios
        public ActionResult Index()
        {
            List<CentroAtencionModel> CentroAtencion = CentroAtencionCollection.AsQueryable<CentroAtencionModel>().ToList();
            return View(CentroAtencion);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(string id)
        {
            var CentroAtencionId = Convert.ToDouble(id);  //new ObjectId(id);
            var CentroAtencion = CentroAtencionCollection.AsQueryable<CentroAtencionModel>().SingleOrDefault(x => x.Id == CentroAtencionId);
            return View(CentroAtencion);
        }

        // GET: Funcionarios/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        public ActionResult Create(CentroAtencionModel CentroAtencion)
        {
            try
            {
                CentroAtencionCollection.InsertOne(CentroAtencion);

                return RedirectToAction("Index"); 
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(string id)
        {
            var CentroAtencionId = Convert.ToDouble(id); //new ObjectId(id);
            var CentroAtencion = CentroAtencionCollection.AsQueryable<CentroAtencionModel>().SingleOrDefault(x => x.Id == CentroAtencionId);
            return View(CentroAtencion);
           
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CentroAtencionModel CentroAtencion)
        {
            try
            {
                var filter = Builders<CentroAtencionModel>.Filter.Eq("id_", ObjectId.Parse(id));
                var update = Builders<CentroAtencionModel>.Update.Set("Nombre", CentroAtencion.Nombre);//Se puede agregar mas haciendo un .Set("",) extra
                var result = CentroAtencionCollection.UpdateOne(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(string id)
        {
            var CentroAtencionId = Convert.ToDouble(id); //new ObjectId(id);
            var CentroAtencion = CentroAtencionCollection.AsQueryable<CentroAtencionModel>().SingleOrDefault(x => x.Id == CentroAtencionId);
            return View(CentroAtencion);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, CentroAtencionModel CentroAtencion)
        {
            try
            {
                CentroAtencionCollection.DeleteOne(Builders<CentroAtencionModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
