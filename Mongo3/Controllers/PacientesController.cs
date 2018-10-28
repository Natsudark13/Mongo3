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
    public class PacientesController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<PacienteModel> pacienteCollection;

        public PacientesController()
        {
            dbcontext = new MongoDBContext();
            pacienteCollection = dbcontext.database.GetCollection<PacienteModel>("Pacientes");
        }

        // GET: Funcionarios
        public ActionResult Index()
        {
            List<PacienteModel> pacientes = pacienteCollection.AsQueryable<PacienteModel>().ToList();
            return View(pacientes);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(string id)
        {
            var pacientesId = Convert.ToDouble(id);  //new ObjectId(id);
            var pacientes = pacienteCollection.AsQueryable<PacienteModel>().SingleOrDefault(x => x.Id == pacientesId);
            return View(pacientes);
        }

        // GET: Funcionarios/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        public ActionResult Create(PacienteModel pacientes)
        {
            try
            {
                pacienteCollection.InsertOne(pacientes);

                return RedirectToAction("Login"); 
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(string id)
        {
            var pacienteId = Convert.ToDouble(id); //new ObjectId(id);
            var paciente = pacienteCollection.AsQueryable<PacienteModel>().SingleOrDefault(x => x.Id == pacienteId);
            return View(paciente);
           
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, PacienteModel pacientes)
        {
            try
            {
                var filter = Builders<PacienteModel>.Filter.Eq("id_", ObjectId.Parse(id));
                var update = Builders<PacienteModel>.Update.Set("Nombre", pacientes.Nombre);//Se puede agregar mas haciendo un .Set("",) extra
                var result = pacienteCollection.UpdateOne(filter, update);

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
            var pacientesId = Convert.ToDouble(id); //new ObjectId(id);
            var pacientes = pacienteCollection.AsQueryable<PacienteModel>().SingleOrDefault(x => x.Id == pacientesId);
            return View(pacientes);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, PacienteModel pacientes)
        {
            try
            {
                pacienteCollection.DeleteOne(Builders<PacienteModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(PacienteModel user, string password)
        {
                var usr = pacienteCollection.AsQueryable<PacienteModel>().SingleOrDefault(u => u.Nombre == user.Nombre && u.Contraseña == password);
                //var usr = funcionarioCollection.FindAsync(u => u.Nombre == user.Nombre && u.Contraseña == user.Contraseña);
                if (usr != null)
                {
                    TempData["cedula"] = usr.cedula;
                    
                    return RedirectToAction("CitaPacienteAsync", "Citas");
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                }
                return View();
            
        }
    }
}
