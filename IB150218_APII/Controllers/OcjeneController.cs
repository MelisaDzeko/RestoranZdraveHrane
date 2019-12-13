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
    public class OcjeneController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Ocjene
        public IQueryable<Ocjene> GetOcjenes()
        {
            return db.Ocjenes;
        }

        // GET: api/Ocjene/5
        [ResponseType(typeof(Ocjene))]
        public IHttpActionResult GetOcjene(int id)
        {
            Ocjene ocjene = db.Ocjenes.Find(id);
            if (ocjene == null)
            {
                return NotFound();
            }

            return Ok(ocjene);
        }

        // PUT: api/Ocjene/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOcjene(int id, Ocjene ocjene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ocjene.OcjenaID)
            {
                return BadRequest();
            }

            db.Entry(ocjene).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OcjeneExists(id))
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

        // POST: api/Ocjene
        [ResponseType(typeof(Ocjene))]
        public IHttpActionResult PostOcjene(Ocjene ocjene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InsertOcjena(ocjene.Datum, ocjene.ProizvodID, ocjene.KupacID, ocjene.Ocjena);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ocjene.OcjenaID }, ocjene);
        }

        // DELETE: api/Ocjene/5
        [ResponseType(typeof(Ocjene))]
        public IHttpActionResult DeleteOcjene(int id)
        {
            Ocjene ocjene = db.Ocjenes.Find(id);
            if (ocjene == null)
            {
                return NotFound();
            }

            db.Ocjenes.Remove(ocjene);
            db.SaveChanges();

            return Ok(ocjene);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OcjeneExists(int id)
        {
            return db.Ocjenes.Count(e => e.OcjenaID == id) > 0;
        }
    }
}