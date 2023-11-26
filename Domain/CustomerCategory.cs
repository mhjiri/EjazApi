using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Cs_ID), nameof(Ct_ID))]
    public class CustomerCategory
    {
        public string Cs_ID { get; set; }
        public Guid Ct_ID { get; set; }

        public int CsCt_Ordinal { get; set; }
        public bool CsCt_Active { get; set; } = false;
        public DateTime CsCt_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CsCt_Creator { get; set; }
        public DateTime? CsCt_ModifyOn { get; set; }
        public string CsCt_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Ct_ID")]
        public Category Category { get; set; }

    }
}