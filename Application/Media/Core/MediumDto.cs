using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Media.Core
{
    public class MediumDto
    {
        public Guid Md_ID { get; set; }

        public byte[] Md_Medium { get; set; }
        public string Md_FileType { get; set; }
        public string Md_Extension { get; set; }
        public string Md_Title { get; set; }
        public string Md_Title_Ar { get; set; }
        public string Md_Desc { get; set; }
        public string Md_Desc_Ar { get; set; }
        public int Md_Ordinal { get; set; }

        public bool Md_Active { get; set; } = false;
        public DateTime Md_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Md_Creator { get; set; }
        public DateTime? Md_ModifyOn { get; set; }
        public string Md_Modifier { get; set; }
    }
}