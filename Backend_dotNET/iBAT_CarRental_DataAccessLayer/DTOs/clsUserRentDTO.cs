using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer.DTOs
{
    public class clsUserRentDTO
    {
        public int rentID { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly returnDate { get; set; }
        public int startMileage { get; set; }
        public string notes { get; set; }
        public int vehicleID { get; set; }
        public string plateNumber { get; set; }
        public Decimal dailyRate { get; set; }
        public Byte[] image { get; set; }
        public string manufacture { get; set; }
        public string model { get; set; }
        public string fuelType {  get; set; }

        public string transmissionType { get; set; }

    }
}
