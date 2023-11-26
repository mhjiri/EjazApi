using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AnnouncementMessage
    {
        [Key]
        public Guid AnMs_ID { get; set; }

        public Guid An_ID { get; set; }
        public Guid AnPr_ID { get; set; }
        public Guid AnCh_ID { get; set; }
        public string Cs_ID { get; set; }

        public string AnMs_Status { get; set; }
        public string AnMs_Comments { get; set; }

        public bool AnMs_Active { get; set; } = false;
        public DateTime AnMs_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AnMs_Creator { get; set; }
        public DateTime? AnMs_ModifyOn { get; set; }
        public string AnMs_Modifier { get; set; }


        [ForeignKey("An_ID")]
        public Announcement Announcement { get; set; }
        [ForeignKey("AnPr_ID")]
        public AnnouncementPriority Priority { get; set; }
        [ForeignKey("AnCh_ID")]
        public AnnouncementChannel Channel { get; set; }
        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("AnMs_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AnMs_Modifier")]
        public AppUser Modifier { get; set; }


    }
}

