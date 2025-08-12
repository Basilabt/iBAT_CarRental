using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer.DTOs
{
    public class clsVehicleDTO
    {
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
    }
}
