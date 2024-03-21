using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Cs_ID), nameof(Rw_ID))]
    public class CustomerReward
    { 
        public string Cs_ID { get; set; } 
        public Guid Rw_ID { get; set; }


        public int CsRw_Coins { get; set; }
        public int CsRw_Duration { get; set; }
        public int CsRw_CoinsLeft { get; set; }
        public DateTime? CsRw_ExpiredOn { get; set; }

        public int CsRw_Ordinal { get; set; }
        public bool CsRw_Active { get; set; } = false;
        public DateTime CsRw_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CsRw_Creator { get; set; }
        public DateTime? CsRw_ModifyOn { get; set; }
        public string CsRw_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Rw_ID")]
        public Reward Reward { get; set; }

    }
}