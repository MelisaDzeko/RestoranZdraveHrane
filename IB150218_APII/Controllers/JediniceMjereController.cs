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
using IB150218_APII.Models;

namespace IB150218_APII.Controllers
{
    public class JediniceMjereController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/JediniceMjere
        public List<usp_SelectAllJediniceMjere_Result> GetJediniceMjeres()
        {
            return db.usp_SelectAllJediniceMjere().ToList();
        }

        // GET: api/JediniceMjere/5
        [ResponseType(typeof(JediniceMjere))]
        public IHttpActionResult GetJediniceMjere(int id)
        {
            JediniceMjere jediniceMjere = db.JediniceMjeres.Find(id);
            if (jediniceMjere == null)
            {
                return NotFound();
            }

            return Ok(jediniceMjere);
        }

        // PUT: api/JediniceMjere/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJediniceMjere(int id, JediniceMjere jediniceMjere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jediniceMjere.JedinicaMjereID)
            {
                return BadRequest();
            }

            db.Entry(jediniceMjere).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JediniceMjereExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/JediniceMjere
        [ResponseType(typeof(JediniceMjere))]
        public IHttpActionResult PostJediniceMjere(JediniceMjere jediniceMjere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JediniceMjeres.Add(jediniceMjere);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jediniceMjere.JedinicaMjereID }, jediniceMjere);
        }

        // DELETE: api/JediniceMjere/5
        [ResponseType(typeof(JediniceMjere))]
        public IHttpActionResult DeleteJediniceMjere(int id)
        {
            JediniceMjere jediniceMjere = db.JediniceMjeres.Find(id);
            if (jediniceMjere == null)
            {
                return NotFound();
            }

            db.JediniceMjeres.Remove(jediniceMjere);
            db.SaveChanges();

            return Ok(jediniceMjere);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JediniceMjereExists(int id)
        {
            return db.JediniceMjeres.Count(e => e.JedinicaMjereID == id) > 0;
        }
    }
}