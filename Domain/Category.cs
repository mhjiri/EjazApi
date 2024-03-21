using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Category
    {
        [Key]
        public Guid Ct_ID { get; set; }

        public Guid? Ct_ClassificationID { get; set; }
        public Guid? Md_ID { get; set; }


        public string Ct_Name { get; set; }
        public string Ct_Name_Ar { get; set; }
        public string Ct_Title { get; set; }
        public string Ct_Title_Ar { get; set; }
        public string Ct_Desc { get; set; }
        public string Ct_Desc_Ar { get; set; }

        public bool Ct_Active { get; set; } = false;
        public DateTime Ct_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Ct_Creator { get; set; }
        public DateTime? Ct_ModifyOn { get; set; }
        public string Ct_Modifier { get; set; }


        public Category Classification { get; set; }

        [ForeignKey("Md_ID")]
        public Medium Image { get; set; }
        [ForeignKey("Ct_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Ct_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<BookCategory> Books { get; set; }
        public virtual ICollection<CategoryTag> Tags { get; set; }
        public virtual ICollection<CustomerCategory> Customers { get; set; }
    }
}

