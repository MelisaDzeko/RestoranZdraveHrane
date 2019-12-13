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
    public class MeniController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Meni
        public List<Allmeni_Result> GetMenis()
        {
            return db.Allmeni().ToList();
        }
        

               [HttpGet]
        [Route("api/Meni/SerachMeni/{name?}")]

        public List<serachByNazivmeni_Result> SerachMeni(string name = "")
        {
            return db.serachByNazivmeni(name).ToList();


        }
        // GET: api/Meni/5
        [ResponseType(typeof(Meni))]
        public IHttpActionResult GetMeni(int id)
        {
            Meni meni = db.Menis.Find(id);
            if (meni == null)
            {
                return NotFound();
            }

            return Ok(meni);
        }

        // PUT: api/Meni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeni(int id, Meni meni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meni.MenidID)
            {
                return BadRequest();
            }

            db.UpdateMeni(id, meni.Naziv, meni.Opis, meni.KorisnikID);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeniExists(id))
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

        // POST: api/Meni
        [ResponseType(typeof(Meni))]
        public IHttpActionResult PostMeni(Meni meni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Menis.Add(meni);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meni.MenidID }, meni);
        }

        // DELETE: api/Meni/5
        [ResponseType(typeof(Meni))]
        public IHttpActionResult DeleteMeni(int id)
        {
            Meni meni = db.Menis.Find(id);
            if (meni == null)
            {
                return NotFound();
            }

            db.DeleteMeni(id);

            return Ok(meni);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeniExists(int id)
        {
            return db.Menis.Count(e => e.MenidID == id) > 0;
        }
    }
}