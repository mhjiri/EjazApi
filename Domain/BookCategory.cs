using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(Ct_ID))]
    public class BookCategory
    {
        public Guid Bk_ID { get; set; }
        public Guid Ct_ID { get; set; }

        public int BkCt_Ordinal { get; set; }
        public bool BkCt_Active { get; set; } = false;
        public DateTime BkCt_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkCt_Creator { get; set; }
        public DateTime? BkCt_ModifyOn { get; set; }
        public string BkCt_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Ct_ID")]
        public Category Category { get; set; }
        [ForeignKey("BkCt_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkCt_Modifier")]
        public AppUser Modifier { get; set; }

    }
}