using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Cs_ID), nameof(Gn_ID))]
    public class CustomerGenre
    {
        public string Cs_ID { get; set; }
        public Guid Gn_ID { get; set; }

        public int CsGn_Ordinal { get; set; }
        public bool CsGn_Active { get; set; } = false;
        public DateTime CsGn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string CsGn_Creator { get; set; }
        public DateTime? CsGn_ModifyOn { get; set; }
        public string CsGn_Modifier { get; set; }

        [ForeignKey("Cs_ID")]
        public AppUser Customer { get; set; }
        [ForeignKey("Gn_ID")]
        public Genre Genre { get; set; }

    }
}