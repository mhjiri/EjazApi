using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Announcement
    {
        [Key]
        public Guid An_ID { get; set; }

        public Guid Md_ID { get; set; }

        public string An_Name { get; set; }
        public string An_Name_Ar { get; set; }
        public string An_Title { get; set; }
        public string An_Title_Ar { get; set; }
        public string An_Desc { get; set; }
        public string An_Desc_Ar { get; set; }
        public DateTime? An_SendFrom { get; set; }
        public DateTime? An_SendTill { get; set; }

        public bool An_Active { get; set; } = false;
        public DateTime An_CreatedOn { get; set; } = DateTime.UtcNow;
        public string An_Creator { get; set; }
        public DateTime? An_ModifyOn { get; set; }
        public string An_Modifier { get; set; }

        [ForeignKey("Md_ID")]
        public Medium Image { get; set; }
        [ForeignKey("An_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("An_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<AnnouncementCondition> Conditions { get; set; }
        public virtual ICollection<AnnouncementMessage> Messages { get; set; }


    }
}

