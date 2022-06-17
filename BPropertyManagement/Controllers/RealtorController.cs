using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using BPropertyManagement.Models;
using BPropertyManagement.Models.ViewModels;
using System.Web.Script.Serialization;

namespace BPropertyManagement.Controllers
{
    public class RealtorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RealtorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/realtordata/");
        }


        // GET: Realtor/List
        public ActionResult List()
        {
            //getting a data from realtor data api
            //curl https://localhost:44341/api/realtordata/listrealtors

            
            string url = "listrealtors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<RealtorDto> realtors = response.Content.ReadAsAsync<IEnumerable<RealtorDto>>().Result;
            //Debug.WriteLine("number of Realtor received : ");
            //Debug.WriteLine(realtors.Count());

            return View(realtors);
        }

        // GET: Realtor/Details/5
        public ActionResult Details(int id)
        {
            DetailsRealtor ViewModel = new DetailsRealtor();
            //getting a data from realtor data api
            //curl https://localhost:44341/api/realtordata/findrealtor/{id}


            string url = "findrealtor/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            RealtorDto selectedrealtor = response.Content.ReadAsAsync<RealtorDto>().Result;

            Debug.WriteLine("Realtor received : ");
            Debug.WriteLine(selectedrealtor.RealtorName);

            ViewModel.SelectedRealtor = selectedrealtor;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Realtor/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Realtor/Create
        [HttpPost]
        public ActionResult Create(Realtor realtor)
        {
            Debug.WriteLine("The jsonpayload is : ");
            Debug.WriteLine(realtor.RealtorName);

            // Adding a new Realtor into our system using the API
            //curl -H "Content-type:application/json" -d @realtor.json https://localhost:44341/api/realtordata/addrealtor
            string url = "addrealtor";

            
            string jsonpayload = jss.Serialize(realtor);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

            
        }

        // GET: Realtor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Realtor/Edit/5
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

        // GET: Realtor/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findrealtor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RealtorDto selectedrealtor = response.Content.ReadAsAsync<RealtorDto>().Result;
            return View(selectedrealtor);
        }

        // POST: Realtor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "deleterealtor/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
