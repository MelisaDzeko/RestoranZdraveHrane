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
    public class KorisniciController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Korisnici
        public List<AllKorisnici_Result> GetKorisnicis()
        {
            return db.AllKorisnici().ToList();
        }
        [HttpGet]
        [Route("api/Korisnici/KorisnikByUserName/{username}")]
        public esp_Korisnici_SelectByKorisnickoIme_Result KorisnikByUserName(string username)
        {
            return db.esp_Korisnici_SelectByKorisnickoIme(username).FirstOrDefault();
        }

        // GET: api/Korisnici/5
        [ResponseType(typeof(Korisnici))]
        public IHttpActionResult GetKorisnici(int id)
        {
            Korisnici korisnici = db.Korisnicis.Find(id);
            if (korisnici == null)
            {
                return NotFound();
            }

            return Ok(korisnici);
        }

        // PUT: api/Korisnici/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKorisnici(int id, Korisnici korisnici)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnici.KorisnikID)
            {
                return BadRequest();
            }

            db.esp_Korisnici_Update(korisnici.KorisnikID, korisnici.Ime, korisnici.Prezime, korisnici.Email, korisnici.Telefon, korisnici.KorisnickoIme, korisnici.LozinkaSalt, korisnici.LozinkaHash, korisnici.Status,korisnici.UlogaID);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisniciExists(id))
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

        // POST: api/Korisnici
        [ResponseType(typeof(Korisnici))]
        public IHttpActionResult PostKorisnici(Korisnici korisnici)
        {
            db.esp_Korisnici_Insert(korisnici.Ime,korisnici.Prezime,korisnici.Email,korisnici.Telefon,korisnici.KorisnickoIme,korisnici.LozinkaSalt,korisnici.LozinkaHash,korisnici.UlogaID);

            return CreatedAtRoute("DefaultApi", new { id = korisnici.KorisnikID }, korisnici);
        }

        // DELETE: api/Korisnici/5
        [ResponseType(typeof(Korisnici))]
        public IHttpActionResult DeleteKorisnici(int id)
        {
          
            db.delete_izlazi1(id);

            db.delete_meni(id);
            db.delete_korisnici(id);


            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisniciExists(int id)
        {
            return db.Korisnicis.Count(e => e.KorisnikID == id) > 0;
        }
    }
}