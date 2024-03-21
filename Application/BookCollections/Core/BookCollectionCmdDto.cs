using Application.Books.Core;
using Application.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.BookCollections.Core
{
    public class BookCollectionCmdDto
    {
        public Guid Bc_ID { get; set; }

        public Guid Md_ID { get; set; }


        public string Bc_Title { get; set; }
        public string Bc_Title_Ar { get; set; }
        public string Bc_Desc { get; set; }
        public string Bc_Desc_Ar { get; set; }

        public bool Bc_Active { get; set; } = false;

        public ICollection<BookListDto> Books { get; set; }
        public ICollection<ItemDto> BookItems { get; set; }
    }
}