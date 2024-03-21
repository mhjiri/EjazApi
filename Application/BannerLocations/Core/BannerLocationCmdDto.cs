using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BannerLocations.Core
{
    public class BannerLocationCmdDto
    {
        public Guid Bl_ID { get; set; }

        public string Bl_Title { get; set; }
        public string Bl_Title_Ar { get; set; }
        public string Bl_Desc { get; set; }
        public string Bl_Desc_Ar { get; set; }
        public double Bl_Ratio { get; set; }
        public int Bl_Banners { get; set; }


        public bool Bl_Active { get; set; } = false;
    }
}