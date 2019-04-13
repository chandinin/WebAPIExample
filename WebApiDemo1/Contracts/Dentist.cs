using System.Runtime.Serialization;

namespace WebApiDemo1.Contracts
{
    [DataContract()]
    public class Dentist
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }
    }
}