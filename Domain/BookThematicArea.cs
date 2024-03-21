using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(Th_ID))]
    public class BookThematicArea
    {
        public Guid Bk_ID { get; set; }
        public Guid Th_ID { get; set; }

        public int BkTh_Ordinal { get; set; }
        public bool BkTh_Active { get; set; } = false;
        public DateTime BkTh_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkTh_Creator { get; set; }
        public DateTime? BkTh_ModifyOn { get; set; }
        public string BkTh_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Th_ID")]
        public ThematicArea ThematicArea { get; set; }
        [ForeignKey("BkTh_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkTh_Modifier")]
        public AppUser Modifier { get; set; }

    }
}