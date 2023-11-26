using Application.Core;
using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Banners.Core
{
    public class BannerCmdDto
    {
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
    }
}