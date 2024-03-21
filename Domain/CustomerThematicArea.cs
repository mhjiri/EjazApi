using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    
    public class CustomerThematicArea
    {
        [Key]
        public Guid CsTh_ID { get; set; }

        public string Cs_ID { get; set; }
        public Guid Th_ID { get; set; }

        public int CsTh_Ordinal { get; set; }
        public bool CsTh_Active { get; set; } = false;
        public DateTime CsTh_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CsTh_Creator { get; set; }
        public DateTime? CsTh_ModifyOn { get; set; }
        public string CsTh_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Th_ID")]
        public ThematicArea ThematicArea { get; set; }

    }
}