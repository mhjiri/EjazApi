using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(Bc_ID))]
    public class BookBookCollection
    {
        public Guid Bk_ID { get; set; }
        public Guid Bc_ID { get; set; }

        public int BkBc_Ordinal { get; set; }
        public bool BkBc_Active { get; set; } = false;
        public DateTime BkBc_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkBc_Creator { get; set; }
        public DateTime? BkBc_ModifyOn { get; set; }
        public string BkBc_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Bc_ID")]
        public BookCollection Collection { get; set; }
        [ForeignKey("BkBc_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkBc_Modifier")]
        public AppUser Modifier { get; set; }

    }
}