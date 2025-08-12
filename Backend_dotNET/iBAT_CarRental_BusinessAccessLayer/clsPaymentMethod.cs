using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsPaymentMethod
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete =3 
        }

        public int paymentMethodID {  get; set; }
        public string method {  get; set; }
        public enMode mode {  get; set; }

        public clsPaymentMethod()
        {
            this.paymentMethodID = -1;
            this.method = "";
            this.mode = enMode.AddNew;
        }

        private clsPaymentMethod(int paymentMethodID, string method)
        {
            this.paymentMethodID = paymentMethodID;
            this.method = method;
            this.mode = enMode.Update;
        }

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
