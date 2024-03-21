using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AnnouncementPriority
    {
        [Key]
        public Guid AnPr_ID { get; set; }

        public string AnPr_Name { get; set; }
        public string AnPr_Name_Ar { get; set; }
        public string AnPr_Title { get; set; }
        public string AnPr_Title_Ar { get; set; }
        public string AnPr_Desc { get; set; }
        public string AnPr_Desc_Ar { get; set; }
        public int AnPr_Weight { get; set; }

        public bool AnPr_Active { get; set; } = false;
        public DateTime AnPr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AnPr_Creator { get; set; }
        public DateTime? AnPr_ModifyOn { get; set; }
        public string AnPr_Modifier { get; set; }

        [ForeignKey("AnPr_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AnPr_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<AnnouncementCondition> Conditions { get; set; }
        public virtual ICollection<AnnouncementMessage> Messages { get; set; }


    }
}

