namespace iBAT_CarRental.DTOs.Rent
{
    public class clsRentVehicleRequestDTO
    {
        public int vehicleID {  get; set; }
        public int userID { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly returnDate { get; set; }
        public int paymentMethodID { get; set; }
    }
}
