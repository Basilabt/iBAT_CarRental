using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsRentStatus
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int rentID {  get; set; }
        public string status { get; set; }

        public enMode mode { get; set; }

        public clsRentStatus()
        {
            this.rentID = -1;
            this.status = "";
            this.mode = enMode.AddNew;
        }

        private clsRentStatus(int rentID , string status)
        {
            this.rentID = rentID;
            this.status = status;
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
