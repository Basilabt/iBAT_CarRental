using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer.DTOs
{
    public class clsPaymentDTO
    {
        public int paymentID { get; set; }
        public int rentID { get; set; }
        public int paymentMethodID { get; set; }
        public double paidAmount { get; set; }
        public double extraAmount { get; set; }
        public double returnedAmount { get; set; }
    }
}
