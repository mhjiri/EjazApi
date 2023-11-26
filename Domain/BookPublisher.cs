using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(Pb_ID))]
    public class BookPublisher
    {
        [Key]
        public Guid Bk_ID { get; set; }
        [Key]
        public Guid Pb_ID { get; set; }

        public int BkPb_Ordinal { get; set; }
        public bool BkPb_Active { get; set; } = false;
        public DateTime BkPb_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkPb_Creator { get; set; }
        public DateTime? BkPb_ModifyOn { get; set; }
        public string BkPb_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Pb_ID")]
        public Publisher Publisher { get; set; } 
        [ForeignKey("BkPb_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkPb_Modifier")]
        public AppUser Modifier { get; set; }

    }
}