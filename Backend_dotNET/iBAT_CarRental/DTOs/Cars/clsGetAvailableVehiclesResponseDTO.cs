namespace iBAT_CarRental.DTOs.Cars
{
    public class clsGetAvailableVehiclesResponseDTO
    {
        // Vehicle fields
        public int vehicleID { get; set; }
        public string vin { get; set; }
        public string plateNumber { get; set; }
        public int year { get; set; }
        public int seats { get; set; }
        public string color { get; set; }
        public decimal dailyRate { get; set; }
        public bool isAvailable { get; set; }
        public byte[] image { get; set; }
        public int mileage { get; set; }

        // Manufacture details
        public int manufactureID { get; set; }
        public string manufacture { get; set; }
        public byte[] manufactureImage { get; set; }


        // Model details
        public int modelID { get; set; }
        public string model { get; set; }

        // Fuel type details
        public int fuelTypeID { get; set; }
        public int fuelType { get; set; }
        public string fuelDescription { get; set; }

        // Transmission type details
        public int transmissionTypeID { get; set; }
        public int transmissionType { get; set; }
        public string transmissionDescription { get; set; }
    }

}
