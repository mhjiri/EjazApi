
using Application.Core;
using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.PaymentMethods.Core
{
    public class PaymentMethodCmdDto
    {
        public Guid Py_ID { get; set; }

        public string Py_Title { get; set; }
        public string Py_Title_Ar { get; set; }
        public string Py_Desc { get; set; }
        public string Py_Desc_Ar { get; set; }

        public bool Py_Active { get; set; } = false;
    }
}