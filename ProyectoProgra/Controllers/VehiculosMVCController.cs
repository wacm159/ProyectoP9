using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProyectoProgra.Models;

namespace ProyectoProgra.Controllers
{
    public class VehiculosMVCController : Controller
    {
        private ProyectoProEntities db = new ProyectoProEntities();

        // GET: VehiculosMVC
        public ActionResult Index()
        {
            //var vehiculos = db.Vehiculos.Include(v => v.Cliente);
            //return View(vehiculos.ToList());

            IEnumerable<Vehiculos> vehiculos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53378/api/");
                //GET ALUMNOS
                //obtiene asincronamente y espera hasta obetener la data
                var responseTask = client.GetAsync("vehiculosapi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var leer = result.Content.ReadAsAsync<IList<Vehiculos>>();
                    leer.Wait();
                    vehiculos = leer.Result;
                }
                else
                {
                    vehiculos = Enumerable.Empty<Vehiculos>();
                    ModelState.AddModelError(string.Empty, "Error .... Try Again");
                }
            }
            return View(vehiculos.ToList());
        }

        // GET: VehiculosMVC/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Vehiculos vehiculos = db.Vehiculos.Find(id);
            //if (vehiculos == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(vehiculos);
            Vehiculos vehiculos = new Vehiculos();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53378/api/");
                //GET GetAlumnos

                //Obtiene asincronamente y espera hasta obtenet la data

                var responseTask = client.GetAsync("vehiculosapi/" + id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //leer todo el contenido y parsearlo a lista Alumno
                    var leer = result.Content.ReadAsAsync<Vehiculos>();
                    vehiculos = leer.Result;
                }
                else
                {
                    vehiculos = null;
                    ModelState.AddModelError(string.Empty, "Error....");
                }
            }

            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: VehiculosMVC/Create
        public ActionResult Create()
        {
            //ViewBag.id_vehiculo = new SelectList(db.Cliente, "id_cliente", "nit");
            return View();
        }

        // POST: VehiculosMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_vehiculo,placa,marca,modelo,cilindros,llantas,precio,total,id_cliente")] Vehiculos vehiculos)
        {
        //    if (ModelState.IsValid)
        //    {
        //        db.Vehiculos.Add(vehiculos);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.id_vehiculo = new SelectList(db.Cliente, "id_cliente", "nit", vehiculos.id_vehiculo);
        //    return View(vehiculos);
        if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:53378/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Vehiculos>("vehiculosapi", vehiculos);
                postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Error en la insercion, favor contacte al administrador");

            }
            return RedirectToAction("Index");
        }

        // GET: VehiculosMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_vehiculo = new SelectList(db.Cliente, "id_cliente", "nit", vehiculos.id_vehiculo);
            return View(vehiculos);
        }

        // POST: VehiculosMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_vehiculo,placa,marca,modelo,cilindros,llantas,precio,total,id_cliente")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_vehiculo = new SelectList(db.Cliente, "id_cliente", "nit", vehiculos.id_vehiculo);
            return View(vehiculos);
        }

        // GET: VehiculosMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: VehiculosMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
