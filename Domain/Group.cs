using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Group
    {
        [Key]
        public Guid Gr_ID { get; set; }

        public string Gr_Title { get; set; }
        public string Gr_Title_Ar { get; set; }
        public string Gr_Desc { get; set; }
        public string Gr_Desc_Ar { get; set; }

        public int Gr_AgeFrom { get; set; }
        public int Gr_AgeTill { get; set; }
        public string Gr_Language { get; set; }
        public string Gr_Gender { get; set; }


        public bool Gr_Active { get; set; } = false;
        public DateTime Gr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Gr_Creator { get; set; }
        public DateTime? Gr_ModifyOn { get; set; }
        public string Gr_Modifier { get; set; }


        [ForeignKey("Gr_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Gr_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<GroupCategory> Categories { get; set; }
        public virtual ICollection<GroupThematicArea> ThematicAreas { get; set; }
        public virtual ICollection<GroupGenre> Genres { get; set; }
        public virtual ICollection<GroupTag> Tags { get; set; }
        public virtual ICollection<BannerGroup> Banners { get; set; }
    }
}