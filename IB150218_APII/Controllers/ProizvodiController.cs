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
    public class ProizvodiController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Proizvodi
        public List<AllProizvodiVjezba1_Result> GetProizvodis()
        {

            List<AllProizvodiVjezba1_Result> proizvodi = db.AllProizvodiVjezba1().ToList();

            return proizvodi;
        }
        [HttpGet]
        [ResponseType(typeof(Proizvodi))]

        [Route("api/Proizvodi/Recommended/{proizvodID}")]
        public List<Recommended_Result> Recommended(int proizvodID)
        {
            Preporuka p = new Preporuka();
            return p.GetSlicniProizvodi(proizvodID);
        }

        // vjezba 2
        [HttpGet]
        [ResponseType(typeof(Proizvodi))]

        [Route("api/Proizvodi/ProizvodByJediniceMjereID/{jedinicaMjereID}")]
        public List<Proizvodi_SelectByJedinicaMjere_Result> ProizvodByJediniceMjereID(int jedinicaMjereID)
        {
            List<Proizvodi_SelectByJedinicaMjere_Result> proizvodi = db.Proizvodi_SelectByJedinicaMjere(jedinicaMjereID).ToList();
            return proizvodi;
        }

        //vjezba 3
        [HttpGet]
        [ResponseType(typeof(Proizvodi))]

        [Route("api/Proizvodi/ProizvodByVrstaID/{jedinicaMjereID}")]
        public List<esp_Proizvodi_SelectByVrsta_Result> ProizvodByVrstaID(int VrstaID)
        {
            List<esp_Proizvodi_SelectByVrsta_Result> proizvodi = db.esp_Proizvodi_SelectByVrsta(VrstaID).ToList();
            return proizvodi;
        }


        [HttpGet]
        [ResponseType(typeof(Proizvodi))]

        [Route("api/Proizvodi/ProizvodById/{proizvodID}")]
        public List<ProizvodById_Result> ProizvodById(int proizvodID)
        {
            List<ProizvodById_Result> oprema = db.ProizvodById(proizvodID).ToList();
            return oprema;
        }
        [HttpGet]
        [Route("api/Proizvodi/ProizvodiByVrstaMjera/{vrstaID}/{mjeraID}")]
        public List<esp_selectFromProizvodiReport_Result> ProizvodiByVrstaMjera(int vrstaID,int mjeraID)
        {
            List<esp_selectFromProizvodiReport_Result> proizvodi = db.esp_selectFromProizvodiReport(vrstaID, mjeraID).ToList();
            return proizvodi;
        }
        [HttpGet]
        [Route("api/Proizvodi/ByVrstaID/{typeId}")]
        public List<Select_ByVrstaProizvoda_Result> ByVrstaID(int typeId)
        {
            List<Select_ByVrstaProizvoda_Result> proizvodi = db.Select_ByVrstaProizvoda(typeId).ToList();
            return proizvodi;
        }
//vjezba3
        [HttpGet]
        [Route("api/Proizvodi/ProizvodiByVrstaID/{VrstaID}")]
        public List<Select_ByVrstaProizvoda_Result> ProizvodiByVrstaID(int VrstaID)
        {
            List<Select_ByVrstaProizvoda_Result> proizvodi = db.Select_ByVrstaProizvoda(VrstaID).ToList();
            return proizvodi;
        }
        //vjezba 4
        [HttpGet]
        [Route("api/Proizvodi/Proizvodi_SelectByNaziv/{name?}")]

        public List<esp_Proizvodi_SelectByNaziv_Result> Proizvodi_SelectByNaziv(string name = "")
        {
            return db.esp_Proizvodi_SelectByNaziv(name).ToList();


        }

        [HttpGet]
        [Route("api/Proizvodi/ByVrstaIDMobile/{typeId}")]
        public List<AllProizvodiByVrstaMobile_Result> ByVrstaIDMobile(int typeId)
        {
            List<AllProizvodiByVrstaMobile_Result> proizvodi = db.AllProizvodiByVrstaMobile(typeId).ToList();
            return proizvodi;
        }
        [HttpGet]
        [Route("api/Proizvodi/AllProizvodiForMobile")]
        public List<AllProizvodiMobile_Result> AllProizvodiForMobile()
        {
            List<AllProizvodiMobile_Result> proizvodi = db.AllProizvodiMobile().ToList();
            return proizvodi;
        }
        [HttpGet]
        [Route("api/Proizvodi/AllProizvodiForAdd3")]
        public List<AllProizvodiForAdd3_Result> AllProizvodiForAdd3()
        {
            List<AllProizvodiForAdd3_Result> proizvodi = db.AllProizvodiForAdd3().ToList();
            return proizvodi;
        }
        // GET: api/Proizvodi/5
        [ResponseType(typeof(Proizvodi))]
        public IHttpActionResult GetProizvodi(int id)
        {
            Proizvodi proizvodi = db.Proizvodis.Find(id);
            if (proizvodi == null)
            {
                return NotFound();
            }

            return Ok(proizvodi);
        }

        // PUT: api/Proizvodi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProizvodi(int id, Proizvodi proizvodi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proizvodi.ProizvodID)
            {
                return BadRequest();
            }

            db.Entry(proizvodi).State = EntityState.Modified;
           
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProizvodiExists(id))
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

        // POST: api/Proizvodi
        [ResponseType(typeof(Proizvodi))]
        public IHttpActionResult PostProizvodi(Proizvodi proizvodi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.esp_Proizvodi_Insert(proizvodi.Naziv,proizvodi.Sifra,proizvodi.Cijena,proizvodi.VrstaID,proizvodi.JedinicaMjereID,proizvodi.Slika,proizvodi.SlikaThumb);

            return CreatedAtRoute("DefaultApi", new { id = proizvodi.ProizvodID }, proizvodi);
        }

        // DELETE: api/Proizvodi/5
        [ResponseType(typeof(Proizvodi))]
        public IHttpActionResult DeleteProizvodi(int id)
        {
            Proizvodi proizvodi = db.Proizvodis.Find(id);
            if (proizvodi == null)
            {
                return NotFound();
            }

            db.Proizvodis.Remove(proizvodi);
            db.SaveChanges();

            return Ok(proizvodi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProizvodiExists(int id)
        {
            return db.Proizvodis.Count(e => e.ProizvodID == id) > 0;
        }
    }
}