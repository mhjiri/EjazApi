using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Publisher
    {
        [Key]
        public Guid Pb_ID { get; set; }

        public Guid? Md_ID { get; set; }
        public Guid Cn_ID { get; set; }


        public string Pb_Name { get; set; }
        public string Pb_Name_Ar { get; set; }
        public string Pb_Title { get; set; }
        public string Pb_Title_Ar { get; set; }
        public string Pb_Desc { get; set; }
        public string Pb_Desc_Ar { get; set; }

        public bool Pb_Active { get; set; } = false;
        public DateTime Pb_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Pb_Creator { get; set; }
        public DateTime? Pb_ModifyOn { get; set; }
        public string Pb_Modifier { get; set; }

        [ForeignKey("Md_ID")]
        public Medium BrandLogo { get; set; }
        [ForeignKey("Cn_ID")]
        public Country Country { get; set; }
        [ForeignKey("Pb_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Pb_Modifier")]
        public AppUser Modifier { get; set; }

         public virtual ICollection<PublisherGenre> Genres { get; set; }
        public virtual ICollection<BookPublisher> Books { get; set; }
    }
}

