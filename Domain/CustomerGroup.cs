using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Cs_ID), nameof(Gr_ID))]
    public class CustomerGroup
    {
        public string Cs_ID { get; set; }
        public Guid Gr_ID { get; set; }

        public int CsGr_Ordinal { get; set; }
        public bool CsGr_Active { get; set; } = false;
        public DateTime CsGr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CsGr_Creator { get; set; }
        public DateTime? CsGr_ModifyOn { get; set; }
        public string CsGr_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Gr_ID")]
        public Group Group { get; set; }

    }
}