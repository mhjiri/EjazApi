using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Genres.Core
{
    public class GenreDto
    {
        public Guid Gn_ID { get; set; }

        public string Gn_Title { get; set; }
        public string Gn_Title_Ar { get; set; }
        public string Gn_Desc { get; set; }
        public string Gn_Desc_Ar { get; set; }
        public int Gn_Total { get; set; }
        public int Gn_Summaries { get; set; }
        public int Gn_Authors { get; set; }
        public int Gn_Publishers { get; set; }


        public bool Gn_Active { get; set; } = false;
        public DateTime Gn_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Gn_Creator { get; set; }
        public DateTime? Gn_ModifyOn { get; set; }
        public string Gn_Modifier { get; set; }
    }
    
}