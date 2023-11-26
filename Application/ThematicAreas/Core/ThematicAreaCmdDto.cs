using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ThematicAreas.Core
{
    public class ThematicAreaCmdDto
    {
        public Guid Th_ID { get; set; }

        public string Th_Title { get; set; }
        public string Th_Title_Ar { get; set; }
        public string Th_Desc { get; set; }
        public string Th_Desc_Ar { get; set; }

        public bool Th_Active { get; set; } = false;
        public DateTime? Th_ModifyOn { get; set; }
        public string Th_Modifier { get; set; }
    }
}