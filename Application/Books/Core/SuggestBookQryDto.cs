using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Core
{
    public class SuggestBookQryDto
    {
        public Guid Bk_ID { get; set; }
        public string Bk_Code { get; set; }
        public string Bk_Name { get; set; }
        public string Bk_Name_Ar { get; set; }
        public string Bk_Title_Ar { get; set; }
        public Boolean Bk_Active { get; set; }
        public string Bk_Trial { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Language { get; set; }
        public string Bk_Author { get; set; }
        public string Bk_Editor { get; set; }
        public string Bk_Comments { get; set; }
        public DateTime Bk_CreatedOn { get; set; } = DateTime.Now;
        public string Bk_Creator { get; set; }
    }
}
