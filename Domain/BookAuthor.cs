using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(At_ID))]
    public class BookAuthor
    {
        public Guid Bk_ID { get; set; }
        public Guid At_ID { get; set; }

        public int BkAt_Ordinal { get; set; }
        public bool BkAt_Active { get; set; } = false;
        public DateTime BkAt_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkAt_Creator { get; set; }
        public DateTime? BkAt_ModifyOn { get; set; }
        public string BkAt_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("At_ID")]
        public Author Author { get; set; }
        [ForeignKey("BkAt_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkAt_Modifier")]
        public AppUser Modifier { get; set; }

    }
}