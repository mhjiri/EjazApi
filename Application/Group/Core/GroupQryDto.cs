using Application.Categories.Core;
using Application.Core;
using Application.Genres.Core;
using Application.Tags.Core;
using Application.ThematicAreas.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Groups.Core
{
    public class GroupQryDto
    {
        public Guid Gr_ID { get; set; }


        public string Gr_Title { get; set; }
        public string Gr_Title_Ar { get; set; }
        public string Gr_Desc { get; set; }
        public string Gr_Desc_Ar { get; set; }

        public int Gr_AgeFrom { get; set; }
        public int Gr_AgeTill { get; set; }
        public string Gr_Language { get; set; }
        public string Gr_Gender { get; set; }

        public bool Gr_Active { get; set; } = false;
        public DateTime Gr_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Gr_Creator { get; set; }
        public DateTime Gr_ModifyOn { get; set; }
        public string Gr_Modifier { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }
        public ICollection<ItemDto> CategoryItems { get; set; }
        public ICollection<ThematicAreaDto> ThematicAreas { get; set; }
        public ICollection<ItemDto> ThematicAreaItems { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<ItemDto> GenreItems { get; set; }
        public ICollection<TagDto> Tags { get; set; }
        public ICollection<ItemDto> TagItems { get; set; }
    }
}