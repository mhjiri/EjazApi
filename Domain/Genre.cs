using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Genre
    {
        [Key]
        public Guid Gn_ID { get; set; }

        public string Gn_Title { get; set; }
        public string Gn_Title_Ar { get; set; }
        public string Gn_Desc { get; set; }
        public string Gn_Desc_Ar { get; set; }

        public bool Gn_Active { get; set; } = false;
        public DateTime Gn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Gn_Creator { get; set; }
        public DateTime? Gn_ModifyOn { get; set; }
        public string Gn_Modifier { get; set; }

        [ForeignKey("Gn_Creator")]
        public AppUser Creator { get; set; }
        public AppUser Modifier { get; set; }

        public virtual ICollection<CustomerGenre> Customers { get; set; }
        public virtual ICollection<AuthorGenre> Authors { get; set; }
        public virtual ICollection<BookGenre> Books { get; set; }
        public virtual ICollection<PublisherGenre> Publishers { get; set; }
    }
}