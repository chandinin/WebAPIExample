using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo1.Converters
{
    public static class PatientConverter
    {
        public static Contracts.Patient ToContract(this Models.Patient patient)
        {
            if (patient == null)
            {
                return null;
            }

            return new Contracts.Patient()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                Phone = patient.Phone,
                Address = patient.Address,
                DentistId = patient.DentistId
            };
        }

        public static Models.Patient ToDatabase(this Contracts.Patient patient)
        {
            if (patient == null)
            {
                return null;
            }

            return new Models.Patient()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                Phone = patient.Phone,
                Address = patient.Address
            };
        }
    }
}