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
    public class NarudzbaStavkesController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/NarudzbaStavkes
        public IQueryable<NarudzbaStavke> GetNarudzbaStavkes()
        {
            return db.NarudzbaStavkes;
        }

        // GET: api/NarudzbaStavkes/5
        [ResponseType(typeof(NarudzbaStavke))]
        public IHttpActionResult GetNarudzbaStavke(int id)
        {
            NarudzbaStavke narudzbaStavke = db.NarudzbaStavkes.Find(id);
            if (narudzbaStavke == null)
            {
                return NotFound();
            }

            return Ok(narudzbaStavke);
        }

        // PUT: api/NarudzbaStavkes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNarudzbaStavke(int id, NarudzbaStavke narudzbaStavke)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != narudzbaStavke.NarudzbaStavkaID)
            {
                return BadRequest();
            }

            db.Entry(narudzbaStavke).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarudzbaStavkeExists(id))
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

        // POST: api/NarudzbaStavkes
        [ResponseType(typeof(NarudzbaStavke))]
        public IHttpActionResult PostNarudzbaStavke(NarudzbaStavke narudzbaStavke)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.esp_NarudzbaStavke_Insert(narudzbaStavke.NarudzbaID, narudzbaStavke.ProizvodID, narudzbaStavke.Kolicina);


            return CreatedAtRoute("DefaultApi", new { id = narudzbaStavke.NarudzbaStavkaID }, narudzbaStavke);
        }

        // DELETE: api/NarudzbaStavkes/5
        [ResponseType(typeof(NarudzbaStavke))]
        public IHttpActionResult DeleteNarudzbaStavke(int id)
        {
            NarudzbaStavke narudzbaStavke = db.NarudzbaStavkes.Find(id);
            if (narudzbaStavke == null)
            {
                return NotFound();
            }

            db.NarudzbaStavkes.Remove(narudzbaStavke);
            db.SaveChanges();

            return Ok(narudzbaStavke);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NarudzbaStavkeExists(int id)
        {
            return db.NarudzbaStavkes.Count(e => e.NarudzbaStavkaID == id) > 0;
        }
    }
}