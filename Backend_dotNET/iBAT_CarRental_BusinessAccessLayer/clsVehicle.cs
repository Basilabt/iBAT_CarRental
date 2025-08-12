using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsVehicle
    {
        public enum enMode
        {
            AddNew = 1,
            Update = 2,
            Delete = 3
        }

        public int vehicleID { get; set; } 
        public int manufactureID { get; set; }
        public int modelID { get; set; }
        public int fuelTypeID { get; set; }
        public int transmissionTypeID { get; set; }
        public string vin { get; set; }
        public string plateNumber { get; set; }
        public int year { get; set; }
        public int seats { get; set; }
        public string color { get; set; }
        public decimal dailyRate { get; set; }
        public bool isAvailable { get; set; }
        public byte[] image { get; set; }
        public int mileage { get; set; }

        public enMode mode { get; set; }

        public clsModel model { get; set; }
        public clsManufacture manufacture { get; set; }
        public clsFuelType fuelType { get; set; }

        public clsTransmissionType transmissionType { get; set; }

        public clsVehicle()
        {
            this.vehicleID = -1;
            this.manufactureID = -1;
            this.modelID = -1;
            this.fuelTypeID = -1;
            this.transmissionTypeID = -1;
            this.vin = "";
            this.plateNumber = "";
            this.year = 0;
            this.seats = 0;
            this.color = "";
            this.dailyRate = 0;
            this.isAvailable = true;
            this.image = Array.Empty<byte>();
            this.mileage = 0;
            this.mode = enMode.AddNew;
            this.model = null;
            this.manufacture = null;
            this.fuelType = null;
            this.transmissionType = null;
        }

        private clsVehicle(int vehicleID, int manufactureID, int modelID, int fuelTypeID, int transmissionTypeID,
            string vin, string plateNumber, int year, int seats, string color, decimal dailyRate,
            bool isAvailable, byte[] image, int mileage)
        {
            this.vehicleID = vehicleID;
            this.manufactureID = manufactureID;
            this.modelID = modelID;
            this.fuelTypeID = fuelTypeID;
            this.transmissionTypeID = transmissionTypeID;
            this.vin = vin;
            this.plateNumber = plateNumber;
            this.year = year;
            this.seats = seats;
            this.color = color;
            this.dailyRate = dailyRate;
            this.isAvailable = isAvailable;
            this.image = image;
            this.mileage = mileage;
            this.mode = enMode.Update;

            this._loadCompostieObjects();
        }

        public bool save()
        {
            switch (this.mode)
            {
                case enMode.AddNew:
                    // Implement insertion logic
                    return false;

                case enMode.Update:
                    // Implement update logic
                    return false;

                case enMode.Delete:
                    // Implement deletion logic
                    return false;
            }

            return false;
        }

        private void _loadCompostieObjects()
        {
            this.model = clsModel.getModelByModelID(new clsModelDTO { modelID = this.modelID });
            this.manufacture = clsManufacture.getManufactureByManufactureID(new clsManufactureDTO { manufactureID = this.manufactureID });
            this.fuelType = clsFuelType.getFuelTypeByFuelTypeID(new clsFuelTypeDTO { fuelTypeID = this.fuelTypeID });
            this.transmissionType = clsTransmissionType.getTransmissionTypeByTransmissionTypeID(new clsTransmissionTypeDTO { transmissionTypeID = this.transmissionTypeID });
        }

        // Static Methods.

        public static List<clsVehicle> getAvailableVehicles()
        {
            List<clsVehicle> list = new List<clsVehicle>();

            foreach (clsVehicleDTO dto in clsVehicleDataAccess.getAvailableVehicles())
            {
                clsVehicle vehicle = new clsVehicle
                {
                    vehicleID = dto.vehicleID,
                    manufactureID = dto.manufactureID,
                    modelID = dto.modelID,
                    fuelTypeID = dto.fuelTypeID,
                    transmissionTypeID = dto.transmissionTypeID,
                    vin = dto.vin,
                    plateNumber = dto.plateNumber,
                    year = dto.year,
                    seats = dto.seats,
                    color = dto.color,
                    dailyRate = dto.dailyRate,
                    isAvailable = dto.isAvailable,
                    image = dto.image,
                    mileage = dto.mileage
                };

                vehicle._loadCompostieObjects();

                list.Add(vehicle);
            }

            return list;
        }


    }
}
