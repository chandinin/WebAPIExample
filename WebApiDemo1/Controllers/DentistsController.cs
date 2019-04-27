using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApiDemo1.Converters;
using WebApiDemo1.Models;
using Dentist = WebApiDemo1.Contracts.Dentist;

namespace WebApiDemo1.Controllers
{
    public class DentistsController : ApiController
    {
        private WebApiDemoDb1Entities db = new WebApiDemoDb1Entities();

        //PUT: api/Dentists
        [Authorize]
        [HttpPut]
        public IHttpActionResult CreateDentist(Dentist dentist)
        {
            using (var db = new WebApiDemoDb1Entities())
            {
                if (!ModelState.IsValid)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                if (db.Dentists.Any(d => d.Phone == dentist.Phone || d.Email == dentist.Email))
                    return BadRequest("Record already exists!");

                    var dbDentist = dentist.ToDatabase();
                    db.Dentists.Add(dbDentist);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        throw;
                    }
            }
            return Ok("Dentist Reord Created Successfully!");
        }

        // GET: api/Dentists
        [Authorize]
        [HttpGet]
        public IEnumerable<Dentist> GetDentists()
        {
            List<Dentist> dentists = new List<Dentist>();

            using (WebApiDemoDb1Entities db = new WebApiDemoDb1Entities())
            {
                var dbDentist = db.Dentists.ToList();

                if (dbDentist.Count == 0)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                foreach (var row in dbDentist)
                {
                    dentists.Add(row.ToContract());
                    row.ToContract();
                }

                return dentists;
            }
        }

        // POST: api/Dentists/1
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetDentist(int id)
        {
            using (WebApiDemoDb1Entities db = new WebApiDemoDb1Entities())
            {
                var dbDentist = db.Dentists.SingleOrDefault(d => d.Id == id);

                if (dbDentist == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                return Ok(dbDentist.ToContract());
            }
        }

        // Post: api/Dentists/5
        [Authorize]
        [HttpPost]
        public IHttpActionResult UpdateDentist(int id, Dentist dentist)
        {
            using (var db = new WebApiDemoDb1Entities())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbDentist = db.Dentists.SingleOrDefault(x => x.Id == id);
                if (dbDentist == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                dbDentist.FirstName = dentist.FirstName;
                dbDentist.LastName = dentist.LastName;
                dbDentist.Email = dentist.Email;
                dbDentist.Phone = dentist.Phone;

                db.SaveChanges();

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return Ok("Dentist Record Updated Successfully!");
        }

        // DELETE: api/Dentists/5
        [Authorize]
        [HttpDelete]
        public IHttpActionResult DeleteDentist(int id)
        {
            using (var db = new WebApiDemoDb1Entities())
            {
                var dbDentist = db.Dentists.SingleOrDefault(d => d.Id == id);
                if (dbDentist == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                var dbPatient = db.Patients.SingleOrDefault(p=> p.DentistId==id);
                if(dbPatient!=null)
                    return BadRequest("Patient Record exists for the Dentist. Update or delete the patient record");

                db.Dentists.Remove(dbDentist);
                db.SaveChanges();
            }

            return Ok("Dentist record Deleted Successfully!");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DentistExists(int id)
        {
            return db.Dentists.Count(e => e.Id == id) > 0;
        }
    }
}