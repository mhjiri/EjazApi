using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Book
    {
        [Key]
        public Guid Bk_ID { get; set; }

        public Guid? Md_AudioEn_ID { get; set; }
        public Guid? Md_AudioAr_ID { get; set; }

        public string Bk_Code { get; set; }
        public string Bk_Name { get; set; }
        public string Bk_Name_Ar { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Title_Ar { get; set; }
        public string Bk_Introduction { get; set; }
        public string Bk_Introduction_Ar { get; set; }
        public string Bk_Summary { get; set; }
        public string Bk_Summary_Ar { get; set; }
        public string Bk_Characters { get; set; }
        public string Bk_Characters_Ar { get; set; }
        public string Bk_Desc { get; set; }
        public string Bk_Desc_Ar { get; set; }
        public string Bk_Language { get; set; }

        public bool Bk_Trial { get; set; } = false;
        public bool Bk_Active { get; set; } = false;
        public DateTime Bk_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bk_Creator { get; set; }
        public DateTime? Bk_ModifyOn { get; set; }
        public string Bk_Modifier { get; set; }

        public int Bk_ViewCount { get; set; }
        public DateTime? Bk_LastViewedOn { get; set; }

        public string Bk_LastViewedBy { get; set; }

        //[ForeignKey("Md_AudioEn_ID")]
        //public Medium? Audio_En { get; set; }
        //[ForeignKey("Md_AudioAr_ID")]
        //public Medium? Audio_Ar { get; set; }
        [ForeignKey("Bk_Creator")]
        public AppUser Creator { get; set; }
        
        [ForeignKey("Bk_Modifier")]
        public AppUser Modifier { get; set; }

        [ForeignKey("Bk_LastViewedBy")]
        public AppUser BookViewer { get; set; }



        public virtual ICollection<BookCategory> Categories { get; set; }
        public virtual ICollection<BookGenre> Genres { get; set; }
        public virtual ICollection<BookPublisher> Publishers { get; set; }
        public virtual ICollection<BookThematicArea> ThematicAreas { get; set; }
        public virtual ICollection<BookAuthor> Authors { get; set; }
        public virtual ICollection<BookTag> Tags { get; set; }
        public virtual ICollection<Medium> Media { get; set; }
    }
}

