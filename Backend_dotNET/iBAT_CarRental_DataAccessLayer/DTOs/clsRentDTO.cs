using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer.DTOs
{
    public class clsRentDTO
    {
        public int rentID { get; set; }
        public int userID { get; set; }
        public int vehicleID { get; set; }
        public int rentStatusID { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly returnDate { get; set; }
        public DateOnly actualReturnDate { get; set; }
        public int numberOfLateDays { get; set; }
        public int startMileage { get; set; }
        public int endMileage { get; set; }
        public int consumedMileage { get; set; }
        public string notes { get; set; }
    }
}
