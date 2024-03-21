using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Genres.Core
{
    public class GenreCmdDto
    {
        public Guid Gn_ID { get; set; }

        public string Gn_Title { get; set; }
        public string Gn_Title_Ar { get; set; }
        public string Gn_Desc { get; set; }
        public string Gn_Desc_Ar { get; set; }


        public bool Gn_Active { get; set; }
        public DateTime? Gn_ModifyOn { get; set; }
        public string Gn_Modifier { get; set; }
    }
}