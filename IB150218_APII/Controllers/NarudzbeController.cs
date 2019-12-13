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
    public class NarudzbeController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Narudzbe
        public IQueryable<Narudzbe> GetNarudzbes()
        {
            return db.Narudzbes;
        }

        public List<AllNarudzbe_Result> AllNarudzbe()
        {

            List<AllNarudzbe_Result> narudzbe = db.AllNarudzbe().ToList();

            return narudzbe;
        }
        [HttpGet]
        [ResponseType(typeof(Narudzbe))]

        [Route("api/Narudzbe/AllNarudzbeDateOdDateDo/{datumOd?}/{datumDo?}")]
        public List<esp_AllNarudzbe_DateOdDateDo_Result> AllNarudzbeDateOdDateDo(DateTime? datumOd, DateTime? datumDo)
        {
            return db.esp_AllNarudzbe_DateOdDateDo(datumOd, datumDo).ToList();
        }
        [HttpGet]
        [Route("api/Narudzbe/GetStavkeNarudzbe/{id}")]
        public List<esp_NarudzbeStavke_SelectByNarudzbaID_Result> GetStavkeNarudzbe(int id)
        {

            return db.esp_NarudzbeStavke_SelectByNarudzbaID(id).ToList();

        }
        [HttpGet]
        [Route("api/Narudzbe/HistorijaNarudzbi/{kupacID?}")]

        public List<esp_HistorijaNarudzbiByKupacID_Result> HistorijaNarudzbi(int kupacID )
        {
            return db.esp_HistorijaNarudzbiByKupacID(kupacID).ToList();


        }
        [HttpGet]
        [Route("api/Narudzbe/GetAktivneNarudzbe")]
        public List<esp_Narudzbe_SelectAktivne_Result> GetAktivneNarudzbe()
        {
            return db.esp_Narudzbe_SelectAktivne().ToList();
        }
        [HttpGet]
        [Route("api/Narudzbe/NarudzbaLast")]

        public NarudzbaLast_Result NarudzbaLast()
        {
            return db.NarudzbaLast().FirstOrDefault();
        }
        // GET: api/Narudzbe/5
        [ResponseType(typeof(Narudzbe))]
        public IHttpActionResult GetNarudzbe(int id)
        {
            Narudzbe narudzbe = db.Narudzbes.Find(id);
            if (narudzbe == null)
            {
                return NotFound();
            }

            return Ok(narudzbe);
        }
       

        // PUT: api/Narudzbe/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNarudzbe(int id, Narudzbe narudzbe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != narudzbe.NarudzbaID)
            {
                return BadRequest();
            }

            db.Entry(narudzbe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarudzbeExists(id))
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

        // POST: api/Narudzbe
        [ResponseType(typeof(Narudzbe))]
        public int PostNarudzbe(Narudzbe narudzbe)
        {
          
          return  narudzbe.NarudzbaID = Convert.ToInt32(db.esp_Narudzbe_Insert(narudzbe.BrojNarudzbe, narudzbe.KupacID, narudzbe.Datum).FirstOrDefault());

           
            
   
           
        }

        // DELETE: api/Narudzbe/5
        [ResponseType(typeof(Narudzbe))]
        public IHttpActionResult DeleteNarudzbe(int id)
        {
            Narudzbe narudzbe = db.Narudzbes.Find(id);
            if (narudzbe == null)
            {
                return NotFound();
            }

            db.Narudzbes.Remove(narudzbe);
            db.SaveChanges();

            return Ok(narudzbe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NarudzbeExists(int id)
        {
            return db.Narudzbes.Count(e => e.NarudzbaID == id) > 0;
        }
    }
}