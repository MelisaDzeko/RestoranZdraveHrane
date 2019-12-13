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
    public class IzlaziController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Izlazi
        public IQueryable<Izlazi> GetIzlazis()
        {
            return db.Izlazis;
        }

        // GET: api/Izlazi/5
        [ResponseType(typeof(Izlazi))]
        public IHttpActionResult GetIzlazi(int id)
        {
            Izlazi izlazi = db.Izlazis.Find(id);
            if (izlazi == null)
            {
                return NotFound();
            }

            return Ok(izlazi);
        }

        // PUT: api/Izlazi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIzlazi(int id, Izlazi izlazi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != izlazi.IzlaziID)
            {
                return BadRequest();
            }

            db.Entry(izlazi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IzlaziExists(id))
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

        // POST: api/Izlazi
        [ResponseType(typeof(Izlazi))]
        public IHttpActionResult PostIzlazi(Izlazi izlaz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.esp_Izlazi_InsertByNarudzbaID(izlaz.NarudzbaID, izlaz.IznosSaPDV, izlaz.IznosBezPDV, izlaz.KorisnikID);
        

            return CreatedAtRoute("DefaultApi", new { id = izlaz.IzlaziID }, izlaz);
        }

        // DELETE: api/Izlazi/5
        [ResponseType(typeof(Izlazi))]
        public IHttpActionResult DeleteIzlazi(int id)
        {
            Izlazi izlazi = db.Izlazis.Find(id);
            if (izlazi == null)
            {
                return NotFound();
            }

            db.Izlazis.Remove(izlazi);
            db.SaveChanges();

            return Ok(izlazi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IzlaziExists(int id)
        {
            return db.Izlazis.Count(e => e.IzlaziID == id) > 0;
        }
    }
}