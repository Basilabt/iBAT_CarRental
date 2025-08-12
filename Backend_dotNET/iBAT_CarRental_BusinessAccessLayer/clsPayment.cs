using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsPayment
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int paymentID {  get; set; }
        public int rentID { get; set; }
        public int paymentMethodID { get; set; }
        public double paidAmount { get; set; }
        public double extraAmount { get; set; }
        public double returnedAmount { get; set; }
        public enMode mode { get; set; }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.AddNew:
                    return false;

                case enMode.Update:
                    return false;

                case enMode.Delete:
                    return false;

            }

            return false;
        }

    }
}
