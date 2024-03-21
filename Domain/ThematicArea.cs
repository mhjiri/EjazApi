using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ThematicArea
    {
        [Key]
        public Guid Th_ID { get; set; }

        public string Th_Title { get; set; }
        public string Th_Title_Ar { get; set; }
        public string Th_Desc { get; set; }
        public string Th_Desc_Ar { get; set; }

        public bool Th_Active { get; set; } = false;
        public DateTime Th_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Th_Creator { get; set; }
        public DateTime? Th_ModifyOn { get; set; }
        public string Th_Modifier { get; set; }

        [ForeignKey("Th_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Th_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<BookThematicArea> Books { get; set; }
        public virtual ICollection<CustomerThematicArea> Customers { get; set; }
    }
}