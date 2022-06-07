using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zarinpal_REST.Models.ZarinPal
{
    public class VerificationResponse
    {
        public bool IsSuccess
        {
            get => Status == 100;
            set => IsSuccess = value;
        }
        public string RefID { get; set; }
        public int Status { get; set; }
    }
}
