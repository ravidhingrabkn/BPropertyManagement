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
    public class RealtorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RealtorData/ListRealtors
        [HttpGet]
        public IEnumerable<RealtorDto> ListRealtors()
        {
            List<Realtor> realtors =  db.realtors.ToList();
            List<RealtorDto> RealtorDtos = new List<RealtorDto>();

            realtors.ForEach(r => RealtorDtos.Add(new RealtorDto(){
                RealtorId = r.RealtorId,
                RealtorName = r.RealtorName,
                Phone = r.Phone,
                Email = r.Email

            }));

            return RealtorDtos;
        }

        // GET: api/RealtorData/FindRealtor/5
        [ResponseType(typeof(Realtor))]
        [HttpGet]
        public IHttpActionResult FindRealtor(int id)
        {
            Realtor realtor = db.realtors.Find(id);
            RealtorDto RealtorDto = new RealtorDto()
            {
                RealtorId = realtor.RealtorId,
                RealtorName = realtor.RealtorName,
                Phone = realtor.Phone,
                Email = realtor.Email
            };
            if (realtor == null)
            {
                return NotFound();
            }

            return Ok(RealtorDto);
        }

        // POST: api/RealtorData/UpdateRealtor/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRealtor(int id, Realtor realtor)
        {
            Debug.WriteLine(" 1 I have reached the update method 1");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("2 I have reached the update method");
                return BadRequest(ModelState);
            }

            if (id != realtor.RealtorId)
            {
                Debug.WriteLine("3 I have reached the update method");
                Debug.WriteLine("GET parameter"+ id);
                Debug.WriteLine("POST parameter" + realtor.RealtorId);
                return BadRequest();
            }

            db.Entry(realtor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealtorExists(id))
                {
                    Debug.WriteLine("4 I have reached the update method");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("5 I have reached the update method");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RealtorData/AddRealtor
        [ResponseType(typeof(Realtor))]
        [HttpPost]
        public IHttpActionResult AddRealtor(Realtor realtor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.realtors.Add(realtor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = realtor.RealtorId }, realtor);
        }

        // POST: api/RealtorData/DaleteRealtor/5
        [ResponseType(typeof(Realtor))]
        [HttpPost]
        public IHttpActionResult DeleteRealtor(int id)
        {
            Realtor realtor = db.realtors.Find(id);
            if (realtor == null)
            {
                return NotFound();
            }

            db.realtors.Remove(realtor);
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

        private bool RealtorExists(int id)
        {
            return db.realtors.Count(e => e.RealtorId == id) > 0;
        }
    }
}