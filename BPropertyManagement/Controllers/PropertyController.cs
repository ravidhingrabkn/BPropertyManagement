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
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PropertyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44341/api/");
        }

        // GET: Property/List
        public ActionResult List()
        {
            //reading data of all the properties from the database
            //curl https://localhost:44341/api/propertydata/listproperty

            
            string url = "propertydata/listproperty";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<PropertyDto> property = response.Content.ReadAsAsync<IEnumerable<PropertyDto>>().Result;

            //Debug.WriteLine("number of property received : ");
            //Debug.WriteLine(property.Count());


            return View(property);
        }

        // GET: Property/Details/5
        public ActionResult Details(int id)
        {
            DetailsProperty ViewModel = new DetailsProperty();
            //reading details of the property from the database
            //curl https://localhost:44341/api/propertydata/FindProperty/{id}

            
            string url = "propertydata/FindProperty/" + id;
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
            string url = "realtordata/listrealtors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<RealtorDto> RealtorOptions = response.Content.ReadAsAsync<IEnumerable<RealtorDto>>().Result;

            return View(RealtorOptions);
        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(Property property)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(property.PropertyName);
            //objectice: Add new property
            //curl -H "Content-Type:application/json" -d https://localhost:44341/api/propertydata/addproperty
            string url = "propertydata/AddProperty";

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
            UpdateProperty ViewModel = new UpdateProperty();

            // Property to be selected 
            string url = "propertydata/findproperty/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PropertyDto selectedProperty = response.Content.ReadAsAsync<PropertyDto>().Result;
            ViewModel.SelectedProperty = selectedProperty;

            url = "realtordata/listrealtors/";
            response = client.GetAsync(url).Result;
            IEnumerable<RealtorDto> RealtorsOptions = response.Content.ReadAsAsync<IEnumerable<RealtorDto>>().Result;

            ViewModel.RealtorsOptions = RealtorsOptions;


            return View(ViewModel);
        }

        // POST: Property/Update/5
        [HttpPost]
        public ActionResult Update(int id, Property property)
        {

            string url = "propertydata/updateproperty/" + id;
            string jsonpayload = jss.Serialize(property);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            Debug.WriteLine(content);

            if(response.IsSuccessStatusCode)
            {
                // TODO: Add update logic here

                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
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
           
            string url = "propertydata/deleteproperty/" + id;
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
