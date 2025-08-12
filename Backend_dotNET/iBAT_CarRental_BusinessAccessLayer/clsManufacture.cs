using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsManufacture
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2, Delete = 3
        }

        public int manufactureID {  get; set; }
        public string manufacture {  get; set; }
        public Byte[] image { get; set; }
        public enMode mode { get; set; }

        public clsManufacture()
        {
            this.manufactureID = -1;
            this.manufacture = "";
            this.image = null;
            this.mode = enMode.AddNew; 
        }

        private clsManufacture(int manufactureID, string manufacture, byte[] image)
        {
            this.manufactureID = manufactureID;
            this.manufacture = manufacture;
            this.image = image;
            this.mode = enMode.Update;
        }

        bool save()
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

        public static List<clsManufactureDTO> getVehiclesManufactures()
        {
            return clsManufactureDataAccess.getVehiclesManufactures();
        }

        public static clsManufacture getManufactureByManufactureID(clsManufactureDTO manufactureDTO)
        {

            if(clsManufactureDataAccess.getManufactureByManufactureID(manufactureDTO))
            { 
                return new clsManufacture { manufactureID = manufactureDTO.manufactureID , manufacture = manufactureDTO.manufacture , image = manufactureDTO.image };
            }

            return null;
        }


    }
}
