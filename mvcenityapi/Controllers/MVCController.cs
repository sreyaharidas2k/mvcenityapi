using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcenityapi;
using System.Net.Http;

namespace mvcenityapi.Controllers
{
    public class MVCController : Controller
    {
        private apidbEntities db = new apidbEntities();

        // GET: MVC
        public ActionResult Index()
        {
            //return View(db.api_tb.ToList());
            IEnumerable<api_tb> employees = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                //HTTP GET
                var responseTask = client.GetAsync("getapi_tbs");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<api_tb>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
                else
                {
                    employees = Enumerable.Empty<api_tb>();
                }
            }
            return View(employees);
        }

        // GET: MVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            api_tb employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                //HTTP GET
                var responseTask = client.GetAsync($"getapi_tbwithid/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<api_tb>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new api_tb();
                }
            }
            return View(employee);
        }

        // GET: MVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Mark")] api_tb api_tb)
        {
            if (ModelState.IsValid)
            {
                //db.api_tb.Add(api_tb);
                //db.SaveChanges();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<api_tb>("postapi_tb", api_tb);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }

            return View(api_tb);
        }

        // GET: MVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            api_tb employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                //HTTP GET
                var responseTask = client.GetAsync($"getapi_tbwithid/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<api_tb>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new api_tb();
                }
            }
            return View(employee);
        }

        // POST: MVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Mark")] api_tb api_tb)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                    var postTask = client.PutAsJsonAsync<api_tb>($"putapi_tbwithid/{api_tb.Id}", api_tb);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(api_tb);
        }

        // GET: MVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            api_tb employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                //HTTP GET
                var responseTask = client.GetAsync($"getapi_tbwithid/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<api_tb>();
                    readTask.Wait();
                    employee = readTask.Result;
                }
                else
                {
                    employee = new api_tb();
                }
            }
            return View(employee);
        }

        // POST: MVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50067/api/API/");
                //HTTP POST
                var postTask = client.DeleteAsync($"deleteapi_tbwithid/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Delete");
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
