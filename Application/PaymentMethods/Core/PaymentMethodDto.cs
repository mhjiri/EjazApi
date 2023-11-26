using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.PaymentMethods.Core
{
    public class PaymentMethodDto
    {
        public Guid Py_ID { get; set; }

        public string Py_Title { get; set; }
        public string Py_Title_Ar { get; set; }
        public string Py_Desc { get; set; }
        public string Py_Desc_Ar { get; set; }

        public bool Py_Active { get; set; } = false;
        public DateTime Py_CreatedOn { get; set; } = DateTime.UtcNow;
        public string Py_Creator { get; set; }
        public DateTime Py_ModifyOn { get; set; }
        public string Py_Modifier { get; set; }
    }
}