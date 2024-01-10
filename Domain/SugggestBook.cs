using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SugggestBook
    {
        [Key]
        public Guid Bk_ID { get; set; }
        public string Bk_Code { get; set; }
        public string Bk_Title { get; set; }
        public string Bk_Language { get; set; }
        public string Bk_Author { get; set; }
        public string Bk_Editor { get; set; }
        public string Bk_Comments { get; set; }
        public DateTime Bk_CreatedOn { get; set; } = DateTime.Now;
        public string Bk_Creator { get; set; }

        [ForeignKey("Bk_Creator")]
        public AppUser Creator { get; set; }
    }
}
