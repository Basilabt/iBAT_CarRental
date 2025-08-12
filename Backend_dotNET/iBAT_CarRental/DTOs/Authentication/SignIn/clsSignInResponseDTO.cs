namespace iBAT_CarRental.DTOs.Authentication.SignIn
{
    public class clsSignInResponseDTO
    {
        public string jwt { get; set; }
        public int userID { get; set; }
        public int personID { get; set; }
        public int maxAgeInSeconds { get; set; }

    }
}
