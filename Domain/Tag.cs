using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Tag
    {
        [Key]
        public Guid Tg_ID { get; set; }

        public string Tg_Title { get; set; }
        public string Tg_Title_Ar { get; set; }
        public string Tg_Desc { get; set; }
        public string Tg_Desc_Ar { get; set; }

        public bool Tg_Active { get; set; } = false;
        public DateTime Tg_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Tg_Creator { get; set; }
        public DateTime? Tg_ModifyOn { get; set; }
        public string Tg_Modifier { get; set; }

        [ForeignKey("Tg_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Tg_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<CategoryTag> Categories { get; set; }
        public virtual ICollection<BookTag> Books { get; set; }
    }
}