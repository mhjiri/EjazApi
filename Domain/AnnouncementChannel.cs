using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AnnouncementChannel
    {
        [Key]
        public Guid AnCh_ID { get; set; }

        public string AnCh_Name { get; set; }
        public string AnCh_Name_Ar { get; set; }
        public string AnCh_Title { get; set; }
        public string AnCh_Title_Ar { get; set; }
        public string AnCh_Desc { get; set; }
        public string AnCh_Desc_Ar { get; set; }

        public bool AnCh_Active { get; set; } = false;
        public DateTime AnCh_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AnCh_Creator { get; set; }
        public DateTime? AnCh_ModifyOn { get; set; }
        public string AnCh_Modifier { get; set; }

        [ForeignKey("AnCh_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AnCh_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<AnnouncementConditionAnnouncementChannel> Conditions { get; set; }


    }
}

