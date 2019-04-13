using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApiDemo1.Converters;
using WebApiDemo1.Models;
using Patient = WebApiDemo1.Contracts.Patient;

namespace WebApiDemo1.Controllers
{
    public class PatientsController : ApiController
    {
        private WebApiDemoDb1Entities db = new WebApiDemoDb1Entities();

        //PUT: api/Patients
        [Authorize]
        [HttpPut]
        public IHttpActionResult CreatePatients(Patient patient) {

            using (var db = new WebApiDemoDb1Entities())
            {
                if(db.Dentists.Any(p=> p.Email== patient.Email || p.Phone == patient.Phone))
                    return BadRequest("Patient record already exists");

                if (!ModelState.IsValid)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                var dbPatient = patient.ToDatabase();
                db.Patients.Add(dbPatient);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
        return Ok("Successfully Created Patient Record!");
        }

        // GET: api/Patients
        [Authorize]
        [HttpGet]
        public IEnumerable<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (WebApiDemoDb1Entities db = new WebApiDemoDb1Entities())
            {
                var dbPatient = db.Patients.ToList();

                if (dbPatient.Count == 0)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                foreach (var row in dbPatient)
                {
                    patients.Add(row.ToContract());
                    row.ToContract();
                }
            }
            return patients;
        }

        //GET: api/Patients/1
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetPatient(int id)
        {
            var dbPatient = db.Patients.SingleOrDefault(p => p.Id == id);

            if (dbPatient == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            return Ok(dbPatient.ToContract());
        }

        // POST: api/Patients
        [Authorize]
        [HttpPost]
        public IHttpActionResult UpdatePatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbPatient = db.Patients.SingleOrDefault(p=> p.Id== id);

            if (dbPatient == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            dbPatient.FirstName = patient.FirstName;
            dbPatient.LastName = patient.LastName;
            dbPatient.Email = patient.Email;
            dbPatient.Phone = patient.Phone;
            dbPatient.Address = patient.Address;
            dbPatient.DentistId = patient.DentistId;

            try
                {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            
            return Ok("Patient Record Successfully Updated!");
        }

        // DELETE: api/Patients/5
        [Authorize]
        [HttpDelete]
        public IHttpActionResult DeletePatient(int id)
        {
            var dbPatient = db.Patients.SingleOrDefault(p=> p.Id == id);
            if (dbPatient == null)
                return BadRequest("Patient record does not exist!");

            db.Patients.Remove(dbPatient);
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateException)
            {
                throw;
            }
            return Ok("Patient Record Successfully Deleted!");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.Id == id) > 0;
        }
    }
}