using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core
{
    public class ItemDto
    {
        

        public ItemDto(Guid It_ID, int It_Ordinal)
        {
            this.It_ID = It_ID;
            this.It_Ordinal = It_Ordinal;
        }

        public Guid It_ID { get; set; }
        public string It_StrID { get; set; }
        public int It_Ordinal { get; set; }
    }
}