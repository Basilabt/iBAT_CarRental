namespace iBAT_CarRental.DTOs.Authentication.SignUp
{
    public class clsSignUpRequestDTO
    {
        public string ssn { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int gender { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
