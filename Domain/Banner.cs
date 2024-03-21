using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Banner
    {
        [Key]
        public Guid Bn_ID { get; set; }

        public Guid Md_ID { get; set; }
        public Guid Bl_ID { get; set; }
        public Guid Gr_ID { get; set; }

        public string Bn_Title { get; set; }
        public string Bn_Title_Ar { get; set; }
        public string Bn_Desc { get; set; }
        public string Bn_Desc_Ar { get; set; }
        public DateTime? Bn_PublishFrom { get; set; }
        public DateTime? Bn_PublishTill { get; set; }

        public bool Bn_Active { get; set; } = false;
        public DateTime Bn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bn_Creator { get; set; }
        public DateTime? Bn_ModifyOn { get; set; }
        public string Bn_Modifier { get; set; }

        [ForeignKey("Bn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Bn_Modifier")]
        public AppUser Modifier { get; set; }

        [ForeignKey("Bl_ID")]
        public BannerLocation BannerLocation { get; set; }
        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }


    }
}

