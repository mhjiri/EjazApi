using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AnnouncementCondition
    {
        [Key]
        public Guid AnCn_ID { get; set; }

        public Guid An_ID { get; set; }
        public Guid AnPr_ID { get; set; }
        public Guid Gr_ID { get; set; }



        public string AnCn_Name { get; set; }
        public string AnCn_Name_Ar { get; set; }
        public string AnCn_Title { get; set; }
        public string AnCn_Title_Ar { get; set; }
        public string AnCn_Desc { get; set; }
        public string AnCn_Desc_Ar { get; set; }

        public bool AnCn_Active { get; set; } = false;
        public DateTime AnCn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AnCn_Creator { get; set; }
        public DateTime? AnCn_ModifyOn { get; set; }
        public string AnCn_Modifier { get; set; }

        [ForeignKey("An_ID")]
        public Announcement Announcement { get; set; }
        [ForeignKey("AnPr_ID")]
        public AnnouncementPriority AnnouncementPriority { get; set; }
        [ForeignKey("AnCn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AnCn_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<AnnouncementConditionAnnouncementChannel> Channels { get; set; }
        public virtual ICollection<AnnouncementConditionGroup> Groups { get; set; }


    }
}

