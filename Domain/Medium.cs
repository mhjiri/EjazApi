using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Medium
    {
        [Key]
        public Guid Md_ID { get; set; }

        public Guid? Bk_ID { get; set; }

        public byte[] Md_Medium { get; set; }
        public string Md_FileName { get; set; }
        public string Md_FileType { get; set; }
        public string Md_Extension { get; set; }
        public string Md_Title { get; set; }
        public string Md_Title_Ar { get; set; }
        public string Md_Desc { get; set; }
        public string Md_Desc_Ar { get; set; }
        public int Md_Ordinal { get; set; }
        public string DownloadURL { get; set; }

        public bool Md_Active { get; set; } = false;
        public DateTime Md_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Md_Creator { get; set; }
        public DateTime? Md_ModifyOn { get; set; }
        public string Md_Modifier { get; set; }

        [ForeignKey("Bk_ID")]
        public Book Book { get; set; }
        [ForeignKey("Md_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Md_Modifier")]
        public AppUser Modifier { get; set; }
    }
}