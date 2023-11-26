using Application.Books.Core;
using Application.Core;
using Application.Genres.Core;
using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Authors.Core
{
    public class AuthorCmdDto
    {
        public Guid At_ID { get; set; }

        public Guid Md_ID { get; set; }


        public string At_Name { get; set; }
        public string At_Name_Ar { get; set; }
        public string At_Title { get; set; }
        public string At_Title_Ar { get; set; }
        public string At_Desc { get; set; }
        public string At_Desc_Ar { get; set; }

        public bool At_Active { get; set; } = false;

        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<ItemDto> GenreItems { get; set; }
        //public ICollection<BookDto> Books { get; set; }
    }
}