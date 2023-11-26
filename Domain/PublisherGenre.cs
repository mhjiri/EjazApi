using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Domain
{
    [PrimaryKey(nameof(Pb_ID), nameof(Gn_ID))]
    public class PublisherGenre
    {
        [Key]
        public Guid Pb_ID { get; set; }
        [Key]
        public Guid Gn_ID { get; set; }

        public int PbGn_Ordinal { get; set; }
        public bool PbGn_Active { get; set; } = false;
        public DateTime PbGn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string PbGn_Creator { get; set; }
        public DateTime? PbGn_ModifiedOn { get; set; } = DateTime.UtcNow;
        public string PbGn_Modifier { get; set; }

        [ForeignKey("Pb_ID")]
        public Publisher Publisher { get; set; }
        [ForeignKey("Gn_ID")]
        public Genre Genre { get; set; }
        [ForeignKey("PbGn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("PbGn_Modifier")]
        public AppUser Modifier { get; set; }

    }
}