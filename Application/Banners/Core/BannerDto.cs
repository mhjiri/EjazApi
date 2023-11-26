using Application.BannerLocations.Core;
using Application.Groups.Core;
using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Banners.Core
{
    public class BannerDto
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
        public string Bn_GroupTitle { get; set; }
        public string Bn_GroupTitle_Ar { get; set; }
        public string Bn_BannerLocationTitle { get; set; }
        public string Bn_BannerLocationTitle_Ar { get; set; }


        public bool Bn_Active { get; set; } = false;
        public DateTime Bn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bn_Creator { get; set; }
        public DateTime? Bn_ModifyOn { get; set; }
        public string Bn_Modifier { get; set; }
    }
}