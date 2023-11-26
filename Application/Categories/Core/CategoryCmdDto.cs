using Application.Core;
using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Categories.Core
{
    public class CategoryCmdDto
    {
        public Guid Ct_ID { get; set; }

        public string Ct_Name { get; set; }
        public string Ct_Name_Ar { get; set; }
        public string Ct_Title { get; set; }
        public string Ct_Title_Ar { get; set; }
        public string Ct_Desc { get; set; }
        public string Ct_Desc_Ar { get; set; }


        public bool Ct_Active { get; set; } = false;

        public Guid Md_ID { get; set; }

        public Guid ClassificationID { get; set; }

        public ICollection<TagDto> Tags { get; set; }
        public ICollection<ItemDto> TagItems { get; set; }
    }
}