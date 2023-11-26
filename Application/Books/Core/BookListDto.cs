using Application.Authors.Core;
using Application.Categories.Core;
using Application.Genres.Core;
using Application.Media.Core;
using Application.Publishers.Core;
using Application.Tags.Core;
using Application.ThematicAreas.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Books.Core
{
    public class BookListDto
    {
        public Guid Bk_ID { get; set; }

        public Guid? Md_ID { get; set; }

        public string Bk_Code { get; set; }
        public string Bk_Name { get; set; }
        public string Bk_Name_Ar { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Title_Ar { get; set; }
        public string Bk_Language { get; set; }

        public bool Bk_Trial { get; set; } = false;
        public bool Bk_Active { get; set; } = false;
        public DateTime Bk_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bk_Creator { get; set; }
        public DateTime? Bk_ModifyOn { get; set; }
        public string Bk_Modifier { get; set; }
        //public virtual ICollection<MediumDto> Media { get; set; }


    }
}