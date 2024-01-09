using Application.Books.Core;
using Application.Core;
using Application.Genres.Core;
using Application.Tags.Core;
using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Payments.Core
{
    public class PaymentCmdDto
    {
        public Guid Pm_ID { get; set; }

        public Guid? Py_ID { get; set; } // Payment Method : Cash, Card, In-App
        public Guid? Sb_ID { get; set; } // Subscription : Monthly, Yearly

        public string pm_Subscriber { get; set; }

        public string Pm_RefernceID { get; set; }

        public double Pm_DisplayPrice { get; set; }
        public int Pm_Days { get; set; }
        public double Pm_Price { get; set; }
        public int Pm_Result { get; set; }

        public int Pm_Ordinal { get; set; }
        public bool Pm_Active { get; set; } = false;

        public string Pm_Creator { get; set; }

    }
}