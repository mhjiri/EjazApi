using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Reward
    {
        [Key]
        public Guid Rw_ID { get; set; }

        public Guid Md_ID { get; set; }



        public string Rw_Name { get; set; }
        public string Rw_Name_Ar { get; set; }
        public string Rw_Title { get; set; }
        public string Rw_Title_Ar { get; set; }
        public string Rw_Desc { get; set; }
        public string Rw_Desc_Ar { get; set; }
        public int Rw_Coins { get; set; }
        public int Rw_Duration { get; set; }

        public bool Rw_Active { get; set; } = false;
        public DateTime Rw_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Rw_Creator { get; set; }
        public DateTime? Rw_ModifyOn { get; set; }
        public string Rw_Modifier { get; set; }

        [ForeignKey("Md_ID")]
        public Medium Image { get; set; }
        [ForeignKey("Rw_Creator")]
        public AppUser Creator { get; set; }
        [ForeignKey("Rw_Modifier")]
        public AppUser Modifier { get; set; }

        public virtual ICollection<CustomerReward> Customers { get; set; }
        public virtual ICollection<RewardGroup> Groups { get; set; }


    }
}

