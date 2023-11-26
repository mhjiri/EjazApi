using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Authors.Core
{
    public class AuthorDto
    {
        public Guid At_ID { get; set; }

        public Guid? Md_ID { get; set; }


        public string At_Name { get; set; }
        public string At_Name_Ar { get; set; }
        public string At_Title { get; set; }
        public string At_Title_Ar { get; set; }
        public string At_Desc { get; set; }
        public string At_Desc_Ar { get; set; }
        public int At_Summaries { get; set; }

        public bool At_Active { get; set; } = false;
        public DateTime At_CreatedOn { get; set; } = DateTime.UtcNow;
        public string At_Creator { get; set; }
        public DateTime At_ModifyOn { get; set; }
        public string At_Modifier { get; set; }
    }
}