using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public Guid Cn_ID { get; set; }
        public Guid Md_ID { get; set; }

        public string Us_DisplayName { get; set; }
        public DateTime Us_DOB { get; set; }
        public string Us_Gender { get; set; }
        public string Us_language { get; set; }
        public string Us_FirebaseUID { get; set; }
        public string Us_FirebaseToken { get; set; }
        public DateTime? Us_SubscriptionExpiryDate { get; set; }
        public DateTime? Us_SubscriptionStartDate { get; set; }
        public int? Us_SubscriptionDays { get; set; }

        public bool Us_Customer { get; set; } = false;
        public bool Us_Admin { get; set; } = false;
        public bool Us_SuperAdmin { get; set; } = false;
        public bool Us_Active { get; set; } = false;
        public bool Us_Deleted { get; set; } = false;
        public DateTime Us_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Us_Creator { get; set; }
        public DateTime? Us_ModifyOn { get; set; }
        public string Us_Modifier { get; set; }
        

        //[ForeignKey("Md_ID")]
        //public Medium Image { get; set; } = new Medium();
        //[ForeignKey("Cn_ID")]
        //public Country Country { get; set; }
        [ForeignKey("Us_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Us_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CustomerThematicArea> ThematicAreas { get; set; }
        public virtual ICollection<CustomerGenre> Genres { get; set; }
        public virtual ICollection<CustomerCategory> Categories { get; set; }
        public virtual ICollection<CustomerTag> Tags { get; set; }

        public virtual ICollection<CustomerGroup> Groups { get; set; }
    }
}