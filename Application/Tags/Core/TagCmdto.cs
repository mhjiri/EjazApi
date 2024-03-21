using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Tags.Core
{
    public class TagCmdDto
    {
        public Guid Tg_ID { get; set; }

        public string Tg_Title { get; set; }
        public string Tg_Title_Ar { get; set; }
        public string Tg_Desc { get; set; }
        public string Tg_Desc_Ar { get; set; }


        public bool Tg_Active { get; set; } = false;
        public DateTime? Tg_ModifyOn { get; set; }
        public string Tg_Modifier { get; set; }
    }
}