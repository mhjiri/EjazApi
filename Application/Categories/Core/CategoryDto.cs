using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Categories.Core
{
    public class CategoryDto
    {
        public Guid Ct_ID { get; set; }

        public string Ct_Name { get; set; }
        public string Ct_Name_Ar { get; set; }
        public string Ct_Title { get; set; }
        public string Ct_Title_Ar { get; set; }
        public string Ct_Desc { get; set; }
        public string Ct_Desc_Ar { get; set; }
        public int Ct_Summaries { get; set; }


        public bool Ct_Active { get; set; } = false;
        public DateTime Ct_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Ct_Creator { get; set; }
        public DateTime? Ct_ModifyOn { get; set; }
        public string Ct_Modifier { get; set; }

        public Guid Md_ID { get; set; }

        public Guid ClassificationID { get; set; }
        public string Classification { get; set; }
        public string Classification_Ar { get; set; }


        public ICollection<TagDto> Tags { get; set; }
    }
}