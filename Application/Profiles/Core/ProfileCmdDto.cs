using Application.Addresses.Core;
using Application.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Profiles.Core
{
    public class ProfileCmdDto
    {
        
        public Guid Md_ID { get; set; }

        
        public string Us_DisplayName { get; set; }
        public DateTime Us_DOB { get; set; }
        public string Us_Gender { get; set; }
        public string Us_language { get; set; }
        //public string Us_Country { get; set; }
        //public string Us_PhoneCode { get; set; }
        //public string Us_PhoneNumber { get; set; }

    }
}