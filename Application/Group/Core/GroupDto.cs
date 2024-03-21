using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Groups.Core
{
    public class GroupDto
    {
        public Guid Gr_ID { get; set; }

        public string Gr_Title { get; set; }
        public string Gr_Title_Ar { get; set; }
        public string Gr_Desc { get; set; }
        public string Gr_Desc_Ar { get; set; }

        public bool Gr_Active { get; set; } = false;
        public DateTime Gr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Gr_Creator { get; set; }
        public DateTime? Gr_ModifyOn { get; set; }
        public string Gr_Modifier { get; set; }
    }
}