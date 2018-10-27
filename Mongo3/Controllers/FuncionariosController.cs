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
    public class FuncionariosController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<FuncionarioModel> funcionarioCollection; 

         public FuncionariosController()
        {
            dbcontext = new MongoDBContext();
            funcionarioCollection = dbcontext.database.GetCollection<FuncionarioModel>("Funcionario");
        }

        // GET: Funcionarios
        public ActionResult Index()
        {
            List<FuncionarioModel> funcionarios = funcionarioCollection.AsQueryable<FuncionarioModel>().ToList();
            return View(funcionarios);
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(string id)
        {
            var funcionarioId = new ObjectId(id);
            var funcionario = funcionarioCollection.AsQueryable<FuncionarioModel>().SingleOrDefault(x => x.Id == funcionarioId);
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        public ActionResult Create(FuncionarioModel funcionario)
        {
            try
            {
                funcionarioCollection.InsertOne(funcionario);

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
            var funcionarioId = new ObjectId(id);
            var funcionario = funcionarioCollection.AsQueryable<FuncionarioModel>().SingleOrDefault(x => x.Id == funcionarioId);
            return View(funcionario);
           
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FuncionarioModel funcionario)
        {
            try
            {
                var filter = Builders<FuncionarioModel>.Filter.Eq("id_", ObjectId.Parse(id));
                var update = Builders<FuncionarioModel>.Update.Set("Nombre", funcionario.Nombre);//Se puede agregar mas haciendo un .Set("",) extra
                var result = funcionarioCollection.UpdateOne(filter, update);

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
            var funcionarioId = new ObjectId(id);
            var funcionario = funcionarioCollection.AsQueryable<FuncionarioModel>().SingleOrDefault(x => x.Id == funcionarioId);
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FuncionarioModel funcionario)
        {
            try
            {
                funcionarioCollection.DeleteOne(Builders<FuncionarioModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logged()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FuncionarioModel user)
        {
            using (ourDbContext db = new ourDbContext())
            {
                
                var usr = funcionarioCollection.AsQueryable<FuncionarioModel>().SingleOrDefault(u => u.Nombre == user.Nombre && u.Contraseña == user.Contraseña);
                //var usr = funcionarioCollection.FindAsync(u => u.Nombre == user.Nombre && u.Contraseña == user.Contraseña);
                
                if (usr != null)
                {
                    if (user.TipoFuncionario == "0")
                    {
                        return RedirectToAction("Index", "Citas");
                    }

                    if (user.TipoFuncionario == "1")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Citas");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                }
                return View();
            }
        }
    }
}
