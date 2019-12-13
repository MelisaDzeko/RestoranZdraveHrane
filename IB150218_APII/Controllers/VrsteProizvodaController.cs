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
    public class VrsteProizvodaController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/VrsteProizvoda
        public List<usp_SelectAllVrstaProizvoda_Result> GetVrsteProizvodas()
        {
            return db.usp_SelectAllVrstaProizvoda().ToList();
        }

        // GET: api/VrsteProizvoda/5
        [ResponseType(typeof(VrsteProizvoda))]
        public IHttpActionResult GetVrsteProizvoda(int id)
        {
            VrsteProizvoda vrsteProizvoda = db.VrsteProizvodas.Find(id);
            if (vrsteProizvoda == null)
            {
                return NotFound();
            }

            return Ok(vrsteProizvoda);
        }

        // PUT: api/VrsteProizvoda/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVrsteProizvoda(int id, VrsteProizvoda vrsteProizvoda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vrsteProizvoda.VrstaID)
            {
                return BadRequest();
            }

            db.Entry(vrsteProizvoda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VrsteProizvodaExists(id))
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

        // POST: api/VrsteProizvoda
        [ResponseType(typeof(VrsteProizvoda))]
        public IHttpActionResult PostVrsteProizvoda(VrsteProizvoda vrsteProizvoda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VrsteProizvodas.Add(vrsteProizvoda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vrsteProizvoda.VrstaID }, vrsteProizvoda);
        }

        // DELETE: api/VrsteProizvoda/5
        [ResponseType(typeof(VrsteProizvoda))]
        public IHttpActionResult DeleteVrsteProizvoda(int id)
        {
            VrsteProizvoda vrsteProizvoda = db.VrsteProizvodas.Find(id);
            if (vrsteProizvoda == null)
            {
                return NotFound();
            }

            db.VrsteProizvodas.Remove(vrsteProizvoda);
            db.SaveChanges();

            return Ok(vrsteProizvoda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VrsteProizvodaExists(int id)
        {
            return db.VrsteProizvodas.Count(e => e.VrstaID == id) > 0;
        }
    }
}