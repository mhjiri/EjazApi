using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Cs_ID), nameof(Tg_ID))]
    public class CustomerTag
    {
        public string Cs_ID { get; set; }
        public Guid Tg_ID { get; set; }

        public int CsTg_Ordinal { get; set; }
        public bool CsTg_Active { get; set; } = false;
        public DateTime CsTg_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CsTg_Creator { get; set; }
        public DateTime? CsTg_ModifyOn { get; set; }
        public string CsTg_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Tg_ID")]
        public Tag Tag { get; set; }

    }
}