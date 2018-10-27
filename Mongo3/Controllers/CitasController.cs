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
    public class CitasController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<CitasModel> CitasCollection;
        private string cedula;

        public CitasController()
        {
            dbcontext = new MongoDBContext();
            CitasCollection = dbcontext.database.GetCollection<CitasModel>("Citas");
        }

        // GET: Funcionarios
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            var filter = Builders<CitasModel>.Filter.Eq("cedula", "0");
            List<CitasModel> result = await CitasCollection.Find(filter).ToListAsync(); 
            //List<CitasModel> Citas = CitasCollection.AsQueryable<CitasModel>().ToList();
            return View(result);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(string id)
        {
            var CitasId = new ObjectId(id);
            var Citas = CitasCollection.AsQueryable<CitasModel>().SingleOrDefault(x => x.Id == CitasId);
            return View(Citas);
        }

        // GET: Funcionarios/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        public ActionResult Create(CitasModel Citas)
        {
            try
            {
                Citas.Estado = "Registrada";
                CitasCollection.InsertOne(Citas);

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
            var CitasId = new ObjectId(id);
            var Citas = CitasCollection.AsQueryable<CitasModel>().SingleOrDefault(x => x.Id == CitasId);
            return View(Citas);
           
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, CitasModel Citas)
        {
            try
            {
                var filter = Builders<CitasModel>.Filter.Eq("id_", ObjectId.Parse(id));
                var update = Builders<CitasModel>.Update.Set("Especialidad", Citas.Especialidad);//Se puede agregar mas haciendo un .Set("",) extra
                var result = CitasCollection.UpdateOne(filter, update);

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
            var CitasId = new ObjectId(id);
            var Citas = CitasCollection.AsQueryable<CitasModel>().SingleOrDefault(x => x.Id == CitasId);
            return View(Citas);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, CitasModel Citas)
        {
            try
            {
                CitasCollection.DeleteOne(Builders<CitasModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("CitaPacienteAsync");
            }
            catch
            {
                return View();
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> CitaPacienteAsync()
        {
            if (TempData["cedula"] != null)
            {
                this.cedula = (string)TempData["cedula"];
                var filter = Builders<CitasModel>.Filter.Eq("cedula", cedula);
                List<CitasModel> result = await CitasCollection.Find(filter).ToListAsync();
                return View(result);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
    }
}
