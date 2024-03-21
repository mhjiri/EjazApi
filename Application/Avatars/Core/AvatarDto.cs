using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Avatars.Core
{
    public class AvatarDto
    {
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
    }
}