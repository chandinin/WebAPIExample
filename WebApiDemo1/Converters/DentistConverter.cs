using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo1.Converters
{
    public static class DentistConverter
    {
        public static Contracts.Dentist ToContract(this Models.Dentist dentist)
        {
            if (dentist == null)
            {
                return null;
            }

            return new Contracts.Dentist()
            {
                Id = dentist.Id,
                FirstName = dentist.FirstName,
                LastName = dentist.LastName,
                Email = dentist.Email,
                Phone = dentist.Phone
            };
        }

        public static Models.Dentist ToDatabase(this Contracts.Dentist dentist)
        {
            if (dentist == null)
            {
                return null;
            }

            return new Models.Dentist()
            {
                FirstName = dentist.FirstName,
                LastName = dentist.LastName,
                Email = dentist.Email,
                Phone = dentist.Phone
            };
        }
    }
}