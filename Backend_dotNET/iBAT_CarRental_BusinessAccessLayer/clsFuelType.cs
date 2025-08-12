using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsFuelType
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int fuelTypeID { get; set; }
        public int type {  get; set; }
        public string description { get; set; }

        public enMode mode { get; set; }

        public clsFuelType()
        {
            this.fuelTypeID = -1;
            this.type = -1;
            this.description = "";
            this.mode = enMode.AddNew;
        }

        private clsFuelType(int fuelTypeID , int type , string description)
        {
            this.fuelTypeID = fuelTypeID;
            this.type = type;
            this.description = description;
            this.mode = enMode.Update;
        }

        public bool save()
        {
            switch(this.mode)
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

        public static clsFuelType getFuelTypeByFuelTypeID(clsFuelTypeDTO fuelTypeDTO)
        {
            if (clsFuelTypeDataAccess.getFuelTypeByFuelTypeID(fuelTypeDTO))
            {
                return new clsFuelType { fuelTypeID = fuelTypeDTO.fuelTypeID, type = fuelTypeDTO.type, description = fuelTypeDTO.description };
            }

            return null;
        }


    }
}
