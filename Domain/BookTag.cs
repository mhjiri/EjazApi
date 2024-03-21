using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(Tg_ID))]
    public class BookTag
    {
        public Guid Bk_ID { get; set; }
        public Guid Tg_ID { get; set; }

        public int BkTg_Ordinal { get; set; }
        public bool BkTg_Active { get; set; } = false;
        public DateTime BkTg_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkTg_Creator { get; set; }
        public DateTime? BkTg_ModifyOn { get; set; }
        public string BkTg_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Tg_ID")]
        public Tag Tag { get; set; }
        [ForeignKey("BkTg_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkTg_Modifier")]
        public AppUser Modifier { get; set; }

    }
}