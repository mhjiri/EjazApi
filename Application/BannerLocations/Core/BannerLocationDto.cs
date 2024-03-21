using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BannerLocations.Core
{
    public class BannerLocationDto
    {
        public Guid Bl_ID { get; set; }

        public string Bl_Title { get; set; }
        public string Bl_Title_Ar { get; set; }
        public string Bl_Desc { get; set; }
        public string Bl_Desc_Ar { get; set; }
        public double Bl_Ratio { get; set; }
        public int Bl_Banners { get; set; }


        public bool Bl_Active { get; set; } = false;
        public DateTime Bl_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bl_Creator { get; set; }
        public DateTime? Bl_ModifyOn { get; set; }
        public string Bl_Modifier { get; set; }
    }
}