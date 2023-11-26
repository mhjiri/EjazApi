using Application.Authors.Core;
using Application.Categories.Core;
using Application.Core;
using Application.Genres.Core;
using Application.Media.Core;
using Application.Publishers.Core;
using Application.Tags.Core;
using Application.ThematicAreas.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Books.Core
{
    public class BookQryDto
    {
        public Guid Bk_ID { get; set; }


        public Guid? Md_ID { get; set; }
        public Guid? Md_AudioEn_ID { get; set; }
        public Guid? Md_AudioAr_ID { get; set; }

        public string Bk_Code { get; set; }
        public string Bk_Name { get; set; }
        public string Bk_Name_Ar { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Title_Ar { get; set; }
        public string Bk_Introduction { get; set; }
        public string Bk_Introduction_Ar { get; set; }
        public string Bk_Summary { get; set; }
        public string Bk_Summary_Ar { get; set; }
        public string Bk_Characters { get; set; }
        public string Bk_Characters_Ar { get; set; }
        public string Bk_Desc { get; set; }
        public string Bk_Desc_Ar { get; set; }
        public string Bk_Language { get; set; }
        public string Bk_Language_Ar { get; set; }

        public bool Bk_Trial { get; set; } = false;
        public bool Bk_Active { get; set; } = false;
        public DateTime Bk_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Bk_Creator { get; set; }
        public DateTime? Bk_ModifyOn { get; set; }
        public string Bk_Modifier { get; set; }

        public virtual ICollection<MediumDto> Media { get; set; }
        public virtual ICollection<ItemDto> MediumItems { get; set; }
        public virtual ICollection<CategoryDto> Categories { get; set; }
        public virtual ICollection<ItemDto> CategoryItems { get; set; }
        public virtual ICollection<GenreDto> Genres { get; set; }
        public virtual ICollection<ItemDto> GenreItems { get; set; }
        public virtual ICollection<PublisherDto> Publishers { get; set; }
        public virtual ICollection<ItemDto> PublisherItems { get; set; }
        public virtual ICollection<ThematicAreaDto> ThematicAreas { get; set; }
        public virtual ICollection<ItemDto> ThematicAreaItems { get; set; }
        public virtual ICollection<AuthorDto> Authors { get; set; }
        public virtual ICollection<ItemDto> AuthorItems { get; set; }
        public virtual ICollection<TagDto> Tags { get; set; }
        public virtual ICollection<ItemDto> TagItems { get; set; }
    }
   
}