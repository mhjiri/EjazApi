using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Rewards.Core
{
    public class RewardDto
    {
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
        public int Rw_Customers { get; set; }
        public int Rw_Groups { get; set; }

        public bool Rw_Active { get; set; } = false;
        public DateTime Rw_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Rw_Creator { get; set; }
        public DateTime? Rw_ModifyOn { get; set; }
        public string Rw_Modifier { get; set; }
    }
}