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
    public class UlogeController : ApiController
    {
        private eProdajaEntities db = new eProdajaEntities();

        // GET: api/Uloge
        public List<esp_Uloge_SelectAll_Result> GetUloges()
        {
            return db.esp_Uloge_SelectAll().ToList();
        }

        // GET: api/Uloge/5
        [ResponseType(typeof(Uloge))]
        public IHttpActionResult GetUloge(int id)
        {
            Uloge uloge = db.Uloges.Find(id);
            if (uloge == null)
            {
                return NotFound();
            }

            return Ok(uloge);
        }

        // PUT: api/Uloge/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUloge(int id, Uloge uloge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uloge.UlogaID)
            {
                return BadRequest();
            }

            db.Entry(uloge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UlogeExists(id))
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

        // POST: api/Uloge
        [ResponseType(typeof(Uloge))]
        public IHttpActionResult PostUloge(Uloge uloge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Uloges.Add(uloge);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uloge.UlogaID }, uloge);
        }

        // DELETE: api/Uloge/5
        [ResponseType(typeof(Uloge))]
        public IHttpActionResult DeleteUloge(int id)
        {
            Uloge uloge = db.Uloges.Find(id);
            if (uloge == null)
            {
                return NotFound();
            }

            db.Uloges.Remove(uloge);
            db.SaveChanges();

            return Ok(uloge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UlogeExists(int id)
        {
            return db.Uloges.Count(e => e.UlogaID == id) > 0;
        }
    }
}