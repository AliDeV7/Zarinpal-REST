using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zarinpal_REST.Models.ZarinPal
{
    public class PaymentResponse
    {
        public string Authority { set; get; }
        public int Status { set; get; }
        public string PaymentURL { set; get; }
    }
}
