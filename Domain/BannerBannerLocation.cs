using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bn_Id), nameof(Bl_ID))]
    public class BannerBannerLocation
    {
        
        public Guid Bn_Id { get; set; }
        public Guid Bl_ID { get; set; }

        public int BnBl_Ordinal { get; set; }
        public bool BnBl_Active { get; set; } = false;
        public DateTime BnBl_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BnBl_Creator { get; set; }
        public DateTime BnBl_ModifyOn { get; set; }
        public string BnBl_Modifier { get; set; }

        
        [ForeignKey("BnBl_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BnBl_Modifier")]
        public AppUser Modifier { get; set; }

    }
}