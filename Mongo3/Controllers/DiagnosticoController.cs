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
    public class DiagnosticoController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<DiagnosticoModel> DiagnosticoCollection;
       // private IMongoCollection<TratamientoModel> TratamientoCollection;
        public DiagnosticoController()
        {
            dbcontext = new MongoDBContext();
            DiagnosticoCollection = dbcontext.database.GetCollection<DiagnosticoModel>("Diagnostico");
        }

        // GET: Funcionarios
        public ActionResult Index()
        {
            List<DiagnosticoModel> Diagnostico = DiagnosticoCollection.AsQueryable<DiagnosticoModel>().ToList();
            return View(Diagnostico);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(string id)
        {
            var DiagnosticoId = new ObjectId(id);
            var Diagnostico = DiagnosticoCollection.AsQueryable<DiagnosticoModel>().SingleOrDefault(x => x.Id == DiagnosticoId);
            return View(Diagnostico);
        }

        // GET: Funcionarios/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        public ActionResult Create(DiagnosticoModel Diagnostico) //string[] listStrings)
        {
            try
            {
                
                //Diagnostico.Tratamiento = listStrings;
                DiagnosticoCollection.InsertOne(Diagnostico);

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
            var DiagnosticoId = new ObjectId(id);
            var Diagnostico = DiagnosticoCollection.AsQueryable<DiagnosticoModel>().SingleOrDefault(x => x.Id == DiagnosticoId);
            return View(Diagnostico);
           
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, DiagnosticoModel Diagnostico)
        {
            try
            {
                var filter = Builders<DiagnosticoModel>.Filter.Eq("id_", ObjectId.Parse(id));
                var update = Builders<DiagnosticoModel>.Update.Set("Nombre", Diagnostico.Nombre);//Se puede agregar mas haciendo un .Set("",) extra
                var result = DiagnosticoCollection.UpdateOne(filter, update);

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
            var DiagnosticoId = new ObjectId(id);
            var Diagnostico = DiagnosticoCollection.AsQueryable<DiagnosticoModel>().SingleOrDefault(x => x.Id == DiagnosticoId);
            return View(Diagnostico);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, DiagnosticoModel Diagnostico)
        {
            try
            {
                DiagnosticoCollection.DeleteOne(Builders<DiagnosticoModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
