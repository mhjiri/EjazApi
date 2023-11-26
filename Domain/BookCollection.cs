using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class BookCollection
    {
        [Key]
        public Guid Bc_ID { get; set; }

        public Guid Md_ID { get; set; }

        public string Bc_Title { get; set; }
        public string Bc_Title_Ar { get; set; }
        public string Bc_Desc { get; set; }
        public string Bc_Desc_Ar { get; set; }

        public bool Bc_Active { get; set; } = false;
        public DateTime Bc_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bc_Creator { get; set; }
        public DateTime? Bc_ModifyOn { get; set; }
        public string Bc_Modifier { get; set; }


        [ForeignKey("Bc_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Bc_Modifier")]
        public AppUser Modifier { get; set; }

        
        public virtual ICollection<BookBookCollection> Books { get; set; }
    }
}