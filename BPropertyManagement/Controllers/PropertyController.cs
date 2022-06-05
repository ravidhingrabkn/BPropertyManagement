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
    public class PropertyController : Controller
    {
        private static readonly HttpClient client;

        static PropertyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/propertydata/");
        }

        // GET: Property/List
        public ActionResult List()
        {
            //reading data of all the properties from the database
            //curl https://localhost:44341/api/propertydata/listproperty

            
            string url = "listproperty";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<PropertyDto> property = response.Content.ReadAsAsync<IEnumerable<PropertyDto>>().Result;

            //Debug.WriteLine("number of animal received : ");
            //Debug.WriteLine(property.Count());


            return View(property);
        }

        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            DetailsProperty ViewModel = new DetailsProperty();
            //reading details of the property from the database
            //curl https://localhost:44341/api/propertydata/FindProperty/{id}

            
            string url = "FindProperty/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            PropertyDto selectedproperty = response.Content.ReadAsAsync<PropertyDto>().Result;


            
            Debug.WriteLine("Property received : ");
            Debug.WriteLine(selectedproperty.PropertyName);
            ViewModel.SelectedProperty = selectedproperty;

            
            IEnumerable<RealtorDto> Realtors = response.Content.ReadAsAsync<IEnumerable<RealtorDto>>().Result;
            ViewModel.Realtors = Realtors;

            //return View(selectedproperty);
            return View(ViewModel);
        }

        public ActionResult Error() 
        {
            return View();
        }

        // GET: Property/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(Property property)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(property.PropertyName);
            //objectice: Add new property
            //curl -H "Content-Type:application/json" -d https://localhost:44341/api/propertydata/addproperty
            string url = "AddProperty";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(property);

            Debug.WriteLine(jsonpayload);

            
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response =  client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

            
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Property/Edit/5
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

        // GET: Property/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "propertydata/findproperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDto selectedproperty = response.Content.ReadAsAsync<PropertyDto>().Result;
            return View(selectedproperty);
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
           
            string url = "propertydata/deleteproperty/"+id;
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
