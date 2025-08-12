using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsRent
    {
        public enum enMode
        {
            AddNew =1 , Update = 2 , Delete =3
        }

        public int rentID {  get; set; }
        public int userID { get; set; }
        public int vehicleID { get; set; }
        public int rentStatusID { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly returnDate { get; set; }
        public DateOnly actualReturnDate { get; set; }
        public int numberOfLateDays { get; set; }
        public int startMileage { get; set; }
        public int endMileage { get;set; }
        public int consumedMileage { get; set; }
        public string notes { get; set; }

        public enMode mode { get; set; }


        public clsRent()
        {
            this.rentID = -1;
            this.userID = -1;
            this.vehicleID = -1;
            this.rentStatusID = -1;
            this.startDate = DateOnly.MinValue;
            this.returnDate = DateOnly.MinValue;
            this.actualReturnDate = DateOnly.MinValue;
            this.numberOfLateDays = -1;
            this.startMileage = -1;
            this.endMileage = -1;
            this.consumedMileage = -1;
            this.notes = "";
            this.mode = enMode.AddNew;
                
        }

        private clsRent(int rentID, int userID, int vehicleID, int rentStatusID, DateOnly startDate, DateOnly returnDate, DateOnly actualReturnDate, int numberOfLateDays, int startMileage, int endMileage, int consumedMileage, string notes)
        {
            this.rentID = rentID;
            this.userID = userID;
            this.vehicleID = vehicleID;
            this.rentStatusID = rentStatusID;
            this.startDate = startDate;
            this.returnDate = returnDate;
            this.actualReturnDate = actualReturnDate;
            this.numberOfLateDays = numberOfLateDays;
            this.startMileage = startMileage;
            this.endMileage = endMileage;
            this.consumedMileage = consumedMileage;
            this.notes = notes;
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

        // Static Methods

        public static bool rentVehicle(clsVehicleDTO vehicleDTO, clsUserDTO userDTO, clsRentDTO rentDTO, clsPaymentDTO paymentDTO)
        {
            return clsRentDataAccess.rentVehicle(vehicleDTO,  userDTO,  rentDTO,  paymentDTO);
        }

        public static List<clsUserRentDTO> getUserRents(clsUserDTO userDTO)
        {
            return clsRentDataAccess.getUserRents(userDTO);
        }

    }
}
