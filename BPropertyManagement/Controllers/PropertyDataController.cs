using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BPropertyManagement.Models;
using System.Diagnostics;

namespace BPropertyManagement.Controllers
{
    public class PropertyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PropertyData/ListProperty
        [HttpGet]
        public IEnumerable<PropertyDto> ListProperty()
        {
            List<Property> properties =  db.properties.ToList();
            List<PropertyDto> PropertyDtos = new List<PropertyDto>();

            properties.ForEach(p => PropertyDtos.Add(new PropertyDto()
            {
                PropertyId = p.PropertyId,
                PropertyName = p.PropertyName,
                Type = p.Type,
                Style = p.Style,
                Size = p.Size,
                ListPrice = p.ListPrice,
                RealtorName = p.Realtor.RealtorName
            }));

            return PropertyDtos;
        }

        // GET: api/PropertyData/FindProperty/5
        [ResponseType(typeof(Property))]
        [HttpGet]
        public IHttpActionResult FindProperty(int id)
        {
            Property property = db.properties.Find(id);
            PropertyDto PropertyDto = new PropertyDto()
            {
                PropertyId = property.PropertyId,
                PropertyName = property.PropertyName,
                Type = property.Type,
                Style = property.Style,
                Size = property.Size,
                ListPrice = property.ListPrice,
                RealtorName = property.Realtor.RealtorName
            };
            if (property == null)
            {
                return NotFound();
            }

            return Ok(PropertyDto);
        }

        // POST: api/PropertyData/UpdateProperty/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProperty(int id, Property property)
        {
            Debug.WriteLine("I have reached the update property method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("Get parameter"+ id);
                Debug.WriteLine("Post parameter"+ property.PropertyId);
                return BadRequest();
            }

            db.Entry(property).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
                {
                    Debug.WriteLine("property not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("NOne trigger");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PropertyData/AddProperty
        [ResponseType(typeof(Property))]
        [HttpPost]
        public IHttpActionResult AddProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.properties.Add(property);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
        }

        // POST: api/PropertyData/DeleteProperty/5
        [ResponseType(typeof(Property))]
        [HttpPost]
        public IHttpActionResult DeleteProperty(int id)
        {
            Property property = db.properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            db.properties.Remove(property);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}