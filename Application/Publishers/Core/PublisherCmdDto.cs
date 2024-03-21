using Application.Core;
using Application.Genres.Core;
using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Publishers.Core
{
    public class PublisherCmdDto
    {
        public Guid Pb_ID { get; set; }

        public Guid Md_ID { get; set; }
        public Guid Cn_ID { get; set; }


        public string Pb_Name { get; set; }
        public string Pb_Name_Ar { get; set; }
        public string Pb_Title { get; set; }
        public string Pb_Title_Ar { get; set; }
        public string Pb_Desc { get; set; }
        public string Pb_Desc_Ar { get; set; }

        public bool Pb_Active { get; set; } = false;

        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<ItemDto> GenreItems { get; set; }
    }
}