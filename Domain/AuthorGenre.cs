using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [PrimaryKey(nameof(At_ID), nameof(Gn_ID))]
    public class AuthorGenre
    {
        public Guid At_ID { get; set; }
        public Guid Gn_ID { get; set; }

        public int AtGn_Ordinal { get; set; }
        public bool AtGn_Active { get; set; } = false;
        public DateTime AtGn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string AtGn_Creator { get; set; }
        public DateTime? AtGn_ModifiedOn { get; set; }
        public string AtGn_Modifier { get; set; }

        [ForeignKey("At_ID")]
        public Author Author { get; set; }
        [ForeignKey("Gn_ID")]
        public Genre Genre { get; set; }
        [ForeignKey("AtGn_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("AtGn_Modifier")]
        public AppUser Modifier { get; set; }




    }
}