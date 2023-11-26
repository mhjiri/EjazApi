using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BookCollections.Core
{
    public class BookCollectionDto
    {
        public Guid Bc_ID { get; set; }

        public Guid? Md_ID { get; set; }

        public string Bc_Title { get; set; }
        public string Bc_Title_Ar { get; set; }
        public string Bc_Desc { get; set; }
        public string Bc_Desc_Ar { get; set; }
        public int Bc_Summaries { get; set; }

        public bool Bc_Active { get; set; } = false;
        public DateTime Bc_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bc_Creator { get; set; }
        public DateTime Bc_ModifyOn { get; set; }
        public string Bc_Modifier { get; set; }
    }
}