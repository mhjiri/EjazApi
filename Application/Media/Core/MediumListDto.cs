using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Media.Core
{
    public class MediumListDto
    {
        public Guid Md_ID { get; set; }

        public string Md_FileType { get; set; }
        public string Md_Extension { get; set; }
        public string Md_Title { get; set; }
        public string Md_Title_Ar { get; set; }
    }
}