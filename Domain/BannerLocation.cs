using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class BannerLocation
    {
        [Key]
        public Guid Bl_ID { get; set; }

        public string Bl_Title { get; set; }
        public string Bl_Title_Ar { get; set; }
        public string Bl_Desc { get; set; }
        public string Bl_Desc_Ar { get; set; }

        public double Bl_Ratio { get; set; }

        public bool Bl_Active { get; set; } = false;
        public DateTime Bl_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bl_Creator { get; set; }
        public DateTime? Bl_ModifyOn { get; set; }
        public string Bl_Modifier { get; set; }

        [ForeignKey("Bl_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Bl_Modifier")]
        public AppUser Modifier { get; set; }

        
        public virtual ICollection<Banner> Banners { get; set; }
    }
}