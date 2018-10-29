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
        private IMongoCollection<DiagnosticoModel> DiagnosticoCollection;
        private IMongoCollection<TratamientoModel> TratamientoCollection;

        private string cedula;

        public CitasController()
        {
            dbcontext = new MongoDBContext();
            CitasCollection = dbcontext.database.GetCollection<CitasModel>("Citas");
            DiagnosticoCollection = dbcontext.database.GetCollection<DiagnosticoModel>("Diagnostico");
            TratamientoCollection = dbcontext.database.GetCollection<TratamientoModel>("Tratamientos");
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
                string cedula = (string)TempData["cedula2"]; ;
                Citas.cedula = cedula;
                Citas.Estado = "Registrada";
                CitasCollection.InsertOne(Citas);
                TempData["cedula"] = cedula;
                return RedirectToAction("CitaPacienteAsync");
            }
            catch
            {
                return RedirectToAction("CitaPacienteAsync");
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
                var filter = Builders<CitasModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<CitasModel>.Update.Set("Estado", "Cancelada por paciente");
                var result = CitasCollection.UpdateOne(filter, update);
                TempData["cedula"] = Citas.cedula;
                return RedirectToAction("CitaPacienteAsync");
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
                TempData["cedula2"] = this.cedula;
                return View(result);

            }
            else
            {
                return RedirectToAction("Login", "Pacientes");
            }
        }

        public ActionResult SecretarioV()
        {
            List<CitasModel> Citas = CitasCollection.AsQueryable<CitasModel>().ToList();
            return View(Citas);
        }

        public ActionResult EditAsync(string id)
        {
            var CitasId = new ObjectId(id);
            var Citas = CitasCollection.AsQueryable<CitasModel>().SingleOrDefault(x => x.Id == CitasId);
            return View(Citas);

        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> EditAsync(string id, CitasModel Citas)
        {
            try
            {
                if (Citas.Estado == "0")
                {
                    var filter = Builders<CitasModel>.Filter.Eq("_id", ObjectId.Parse(id));
                    var update = Builders<CitasModel>.Update.Set("Cedula", Citas.cedula).Set("Observacion", Citas.Observacion).Set("Estado", "Asignada");//Se puede agregar mas haciendo un .Set("",) extra
                    var result = await CitasCollection.UpdateOneAsync(filter, update);
                    // var result = CitasCollection.UpdateOne(filter, update);

                    return RedirectToAction("SecretarioV");

                }
                else if (Citas.Estado == "1")
                {
                    var filter = Builders<CitasModel>.Filter.Eq("_id", ObjectId.Parse(id));
                    var update = Builders<CitasModel>.Update.Set("Cedula", Citas.cedula).Set("Observacion", Citas.Observacion).Set("Estado", "Eliminada por centro medico");//Se puede agregar mas haciendo un .Set("",) extra
                    var result = await CitasCollection.UpdateOneAsync(filter, update);
                    // var result = CitasCollection.UpdateOne(filter, update);

                    return RedirectToAction("SecretarioV");
                }
                else
                {
                    return View();
                }



            }
            catch
            {
                return View();
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> CitasDoctorAsync()
        {

            var filter = Builders<CitasModel>.Filter.Eq("Estado", "Registrada") | Builders<CitasModel>.Filter.Eq("Estado", "Asignada");
            List<CitasModel> result = await CitasCollection.Find(filter).ToListAsync();
            //TempData["cedula2"] = this.cedula;
            return View(result);

        }

        public ActionResult RealizarDiag(string id, string cedula)
        {
            var filter = Builders<CitasModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<CitasModel>.Update.Set("Estado", "Realizada");
            var result = CitasCollection.UpdateOne(filter, update);

            TempData["cedulaC"] = cedula;
            return View();
        }

        [HttpPost]
        public ActionResult RealizarDiag(DiagnosticoModel cita/*, string id, string cedula*/)
        {
            string cedulaC = (string)TempData["cedulaC"];
            cita.cedula = cedulaC;
            DiagnosticoCollection.InsertOne(cita);
            TempData["cedulaC"] = cedulaC;
            TempData["Nombre"] = cita.Nombre;
            return RedirectToAction("CrearTrataAsync");

        }

        public ActionResult CrearTrataAsync()
        {
            return View();
        }
         
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CrearTrataAsync(TratamientoModel cita, string choice)
        {
            if (choice == "0")
            {

               cedula = (string)TempData["cedulaC"];
               string Nombre = (string)TempData["Nombre"];
               var diag = DiagnosticoCollection.AsQueryable<DiagnosticoModel>().SingleOrDefault(x => x.cedula == cedula && x.Nombre == Nombre);
                /*List<String> xx = null;
                if (diag.Tratamiento == null)
                {
                    xx = new List<string>();
                    xx.Add(cita.ID);
                }
                else {
                    xx = diag.Tratamiento;
                    xx.Add(cita.ID);
                }*/
                if (diag.Tratamiento == null)
                {
                    var filter = Builders<DiagnosticoModel>.Filter.Eq("_id", diag.Id);
                    var update = Builders<DiagnosticoModel>.Update.Set("Tratamiento", new List<string>());
                    var res = await DiagnosticoCollection.UpdateOneAsync(filter, update);

                    var filter2 = Builders<DiagnosticoModel>.Filter.Eq("_id", diag.Id);
                    var update2 = Builders<DiagnosticoModel>.Update.Push<string>("Tratamiento", cita.ID);
                    var res2 = await DiagnosticoCollection.FindOneAndUpdateAsync(filter2, update2);

                }
                else
                {

                    var filter = Builders<DiagnosticoModel>.Filter.Eq("_id", diag.Id);
                    var update = Builders<DiagnosticoModel>.Update.Push<string>("Tratamiento", cita.ID);
                    var result = await DiagnosticoCollection.FindOneAndUpdateAsync(filter, update);
                }

                TratamientoCollection.InsertOne(cita);
               TempData["cedulaC"] = cedula;
                return RedirectToAction("CrearTrataAsync");
            }
            else
            {
                string Nombre = (string)TempData["Nombre"];
                cedula = (string)TempData["cedulaC"];
                
                var diag = DiagnosticoCollection.AsQueryable<DiagnosticoModel>().SingleOrDefault(x => x.cedula == cedula && x.Nombre == Nombre);
                /*List<String> xx = null;
                if (diag.Tratamiento == null)
                {
                    xx = new List<string>();
                    xx.Add(cita.ID);
                }
                else
                {
                    xx=  diag.Tratamiento;
                    xx.Add(cita.ID);
                }*/

                if (diag.Tratamiento == null)
                {
                    var filter = Builders<DiagnosticoModel>.Filter.Eq("_id", diag.Id);
                    var update = Builders<DiagnosticoModel>.Update.Set("Tratamiento", new List<string>());
                    var res = await DiagnosticoCollection.UpdateOneAsync(filter, update);
                }
                else
                {

                    var filter = Builders<DiagnosticoModel>.Filter.Eq("_id", diag.Id);
                    var update = Builders<DiagnosticoModel>.Update.Push<string>("Tratamiento", cita.ID);
                    var result = await DiagnosticoCollection.FindOneAndUpdateAsync(filter, update);
                }

                TratamientoCollection.InsertOne(cita);
                TempData["cedulaC"] = cedula;
                return RedirectToAction("CitasDoctorAsync");

            }
            
        }
        public ActionResult VerP(string cedula)
        {
            return RedirectToAction("Details", "Pacientes", new { cedulaX = cedula });
        }
    }
    }
