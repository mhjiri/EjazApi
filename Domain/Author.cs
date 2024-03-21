using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Author
    {
        [Key]
        public Guid At_ID { get; set; }

        public Guid? Md_ID { get; set; }


        public string At_Name { get; set; }
        public string At_Name_Ar { get; set; }
        public string At_Title { get; set; }
        public string At_Title_Ar { get; set; }
        public string At_Desc { get; set; }
        public string At_Desc_Ar { get; set; }

        public bool At_Active { get; set; } = false;
        public DateTime At_CreatedOn { get; set; } = DateTime.Now;
        public string At_Creator { get; set; }
        public DateTime? At_ModifyOn { get; set; }
        public string At_Modifier { get; set; }

        [ForeignKey("Md_ID")]
        public Medium Image { get; set; }
        [ForeignKey("At_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("At_Modifier")]
        public AppUser Modifier { get; set; }

        
        public virtual ICollection<AuthorGenre> Genres { get; set; }
        public virtual ICollection<BookAuthor> Books { get; set; }


    }
}

