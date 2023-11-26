using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(Bk_ID), nameof(Gn_ID))]
    public class BookGenre
    {
        public Guid Bk_ID { get; set; }
        public Guid Gn_ID { get; set; }

        public int BkGn_Ordinal { get; set; }
        public bool BkGn_Active { get; set; } = false;
        public DateTime BkGn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string BkGn_Creator { get; set; }
        public DateTime? BkGn_ModifyOn { get; set; }
        public string BkGn_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Gn_ID")]
        public Genre Genre { get; set; }
        [ForeignKey("BkGn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("BkGn_Modifier")]
        public AppUser Modifier { get; set; }

    }
}