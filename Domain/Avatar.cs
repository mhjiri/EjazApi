using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Avatar
    {
        [Key]
        public Guid Av_ID { get; set; }

        public Guid Md_ID { get; set; }

        public string Av_Title { get; set; }
        public string Av_Title_Ar { get; set; }
        public string Av_Desc { get; set; }
        public string Av_Desc_Ar { get; set; }

        public bool Av_Active { get; set; } = false;
        public DateTime Av_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Av_Creator { get; set; }
        public DateTime? Av_ModifyOn { get; set; }
        public string Av_Modifier { get; set; }

        [ForeignKey("Av_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Av_Modifier")]
        public AppUser Modifier { get; set; }
    }
}