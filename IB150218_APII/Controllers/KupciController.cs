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
    public class KupciController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        [HttpGet]
        // GET: api/Kupci
        public List<esp_KupciAll_Result> GetKupcis()
        {
            return db.esp_KupciAll().ToList();
        }
        [HttpGet]
        [Route("api/Kupci/KlijentByKorisnickoIme/{name?}")]

        public esp_Kupci_SelectByKorisnickoIme_Result KlijentByKorisnickoIme(string name = "")
        {
            return db.esp_Kupci_SelectByKorisnickoIme(name).FirstOrDefault();


        }
        [HttpGet]
        [Route("api/Kupci/SelectByKorisnickoIme/{name?}")]

        public List<esp_Kupci_SelectByKorisnickoIme1_Result> SelectByKorisnickoIme(string name = "")
        {
          return  db.esp_Kupci_SelectByKorisnickoIme1(name).ToList();

           
        }
        // GET: api/Kupci/5
        [ResponseType(typeof(Kupci))]
        public IHttpActionResult GetKupci(int id)
        {
            Kupci kupci = db.Kupcis.Find(id);
            if (kupci == null)
            {
                return NotFound();
            }

            return Ok(kupci);
        }

        // PUT: api/Kupci/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKupci(int id, Kupci kupci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kupci.KupacID)
            {
                return BadRequest();
            }

            db.Entry(kupci).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KupciExists(id))
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

        // POST: api/Kupci
        [ResponseType(typeof(Kupci))]
        public IHttpActionResult PostKupci(Kupci kupci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.esp_Kupci_Insert(kupci.Ime, kupci.Prezime, kupci.Email, kupci.KorisnickoIme, kupci.LozinkaSalt, kupci.LozinkaHash);

            return CreatedAtRoute("DefaultApi", new { id = kupci.KupacID }, kupci);
        }

        // DELETE: api/Kupci/5
        [ResponseType(typeof(Kupci))]
        public IHttpActionResult DeleteKupci(int id)
        {
            Kupci kupci = db.Kupcis.Find(id);
            if (kupci == null)
            {
                return NotFound();
            }

            db.Kupcis.Remove(kupci);
            db.SaveChanges();

            return Ok(kupci);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KupciExists(int id)
        {
            return db.Kupcis.Count(e => e.KupacID == id) > 0;
        }
    }
}