using iBAT_CarRental.DTOs.Authentication;
using iBAT_CarRental.JWT;
using iBAT_CarRental_BusinessAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using iBAT_CarRental_DataAccessLayer.DTOs;
using iBAT_CarRental_BusinessAccessLayer;
using FactWebsiteApp.Models.Utility;
using iBAT_CarRental.DTOs.Authentication.SignIn;
using iBAT_CarRental.DTOs.Authentication.SignUp;

using Microsoft.AspNetCore.Authorization;
using iBAT_CarRental.DTOs.Cars;
using iBAT_CarRental.DTOs.Rent;
using iBAT_CarRental.DTOs.Information;
using Microsoft.Owin.Security.Provider;


namespace iBAT_CarRental.Controllers
{
    [Route("api/iBATCarRental")]
    [ApiController]
    public class iBATCarRentalController : ControllerBase
    {

        private readonly clsJWTService _jwtService;

        public iBATCarRentalController(clsJWTService jwtService)
        {
            _jwtService = jwtService;
        }
           

        [HttpPost("User/SignInByEmailAndPassword", Name = "SignInByEmailAndPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SignInByEmailAndPassword([FromBody] clsSignInRequestDTO requestDTO)
        {

            if (string.IsNullOrEmpty(requestDTO.email) || string.IsNullOrEmpty(requestDTO.password))
            { 
                return BadRequest(new clsSignInErrorResponseDTO { isSucceed = false ,message = "Empty username or password" });
            }


            clsUser user = clsUser.signInByEmailAndPassword(new clsPersonDTO { email = requestDTO.email} , new clsUserDTO { password = clsEncryptor.ComputeHash(requestDTO.password) });
            if(user == null)
            {
                return Unauthorized(new clsSignInErrorResponseDTO { isSucceed = false, message = "Incorrect username or password" });
            }

            var jwt = this._jwtService.GenerateToken(requestDTO.email, "Normal"); 

            return Ok(new clsSignInResponseDTO { personID = user.personID , userID = user.userID , jwt = jwt , maxAgeInSeconds = 7200});
        }


        
        [HttpPost("User/SignUp", Name = "SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SignUp([FromBody] clsSignUpRequestDTO requestDTO)
        {
            
            if(requestDTO == null)
            {
                return BadRequest(new clsSignUpResponseDTO { isSucceed = false, message = "Request cannot be null." });
            }

            if (string.IsNullOrWhiteSpace(requestDTO.ssn) ||
                 string.IsNullOrWhiteSpace(requestDTO.firstName) ||
                 string.IsNullOrWhiteSpace(requestDTO.secondName) ||
                 string.IsNullOrWhiteSpace(requestDTO.thirdName) ||
                 string.IsNullOrWhiteSpace(requestDTO.lastName) ||
                 string.IsNullOrWhiteSpace(requestDTO.email) ||
                 string.IsNullOrWhiteSpace(requestDTO.phoneNumber) ||
                 string.IsNullOrWhiteSpace(requestDTO.username) ||
                 string.IsNullOrWhiteSpace(requestDTO.password) ||
                 (requestDTO.gender != 0 && requestDTO.gender != 1)) 
            {
                return BadRequest(new clsSignUpResponseDTO { isSucceed = false, message = "All fields are required and must be valid." });
            }


            if (clsPerson.doesEmailExist(new clsPersonDTO { email = requestDTO.email }))
            {
                return BadRequest(new clsSignUpResponseDTO { isSucceed = false, message = "Email already exists." });
            }

         
            if (clsPerson.doesSSNExist(new clsPersonDTO { ssn = requestDTO.ssn }))
            {
                return BadRequest(new clsSignUpResponseDTO { isSucceed = false, message = "SSN already exists." });
            }

         
            if (clsPerson.doesPhoneNumberExist(new clsPersonDTO { phoneNumber = requestDTO.phoneNumber }))
            {
                return BadRequest(new clsSignUpResponseDTO { isSucceed = false, message = "Phone number already exists." });
            }

            
            if (clsUser.doesUsernameExist(new clsUserDTO { username = requestDTO.username }))
            {
                return BadRequest(new clsSignUpResponseDTO { isSucceed = false, message = "Username already exists." });
            }


          
            if(!clsUser.signUp(new clsPersonDTO { ssn = requestDTO.ssn, firstName = requestDTO.firstName, secondName = requestDTO.secondName, thirdName = requestDTO.thirdName, lastName = requestDTO.lastName, email = requestDTO.email, phoneNumber = requestDTO.phoneNumber, gender = requestDTO.gender }, new clsUserDTO { username = requestDTO.username, password = clsEncryptor.ComputeHash(requestDTO.password) }))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new clsSignUpResponseDTO { isSucceed = false, message = "An unexpected error occurred during signup. Please try again later." });
            }

            return Ok(new clsSignUpResponseDTO { isSucceed = true , message = "Account Created Successfully"});
        }



        [HttpGet("Vehicles/GetVehiclesManufactures", Name = "GetVehiclesManufactures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetVehiclesManufactures()
        {

            List<clsManufactureDTO> list = clsManufacture.getVehiclesManufactures();

            if(list.Count == 0)
            {
                return NotFound("No Manufactures Found");
            }

            return Ok(list);
        }



        [HttpGet("Vehicles/GetAvailableVehicles", Name = "GetAvailableVehicles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAvailableVehicles()
        {
            List<clsVehicle> vehicles = clsVehicle.getAvailableVehicles();

            if (vehicles.Count == 0)
            {
                return NotFound("No Vehicles Found");
            }

            List<clsGetAvailableVehiclesResponseDTO> responseList = new List<clsGetAvailableVehiclesResponseDTO>();

            foreach (clsVehicle vehicle in vehicles)
            {
                clsGetAvailableVehiclesResponseDTO dto = new clsGetAvailableVehiclesResponseDTO
                {
                    vehicleID = vehicle.vehicleID,
                    vin = vehicle.vin,
                    plateNumber = vehicle.plateNumber,
                    year = vehicle.year,
                    seats = vehicle.seats,
                    color = vehicle.color,
                    dailyRate = vehicle.dailyRate,
                    isAvailable = vehicle.isAvailable,
                    image = vehicle.image,
                    mileage = vehicle.mileage,

                    manufactureID = vehicle.manufactureID,
                    manufacture = vehicle.manufacture?.manufacture,
                    manufactureImage = vehicle.manufacture?.image,

                    modelID = vehicle.modelID,
                    model = vehicle.model?.model,

                    fuelTypeID = vehicle.fuelTypeID,
                    fuelType = vehicle.fuelType?.type ?? -1,
                    fuelDescription = vehicle.fuelType?.description,

                    transmissionTypeID = vehicle.transmissionTypeID,
                    transmissionType = vehicle.transmissionType?.type ?? -1,
                    transmissionDescription = vehicle.transmissionType?.description
                };

                responseList.Add(dto);
            }

            return Ok(responseList);
        }




        [HttpPost("Vehicles/RentVehicle", Name = "RentVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RentVehicle([FromBody] clsRentVehicleRequestDTO requestDTO)
        {
            Console.WriteLine($"DEBUG API: VehicleID = {requestDTO.vehicleID} \n UserID = {requestDTO.userID} \n start date = {requestDTO.startDate} \n returnDate = {requestDTO.returnDate} \n PaymentMethodID = {requestDTO.paymentMethodID}");

            if(requestDTO.vehicleID <= 0)
            {
                return BadRequest("Invalid VehivleID");
            }

            if(requestDTO.userID <= 0)
            {
                return BadRequest("Invalid UserID");
            }

            if(requestDTO.paymentMethodID <= 0)
            {
                return BadRequest("Invalid Payment MethodID");
            }

            bool isSucceed = clsRent.rentVehicle(new clsVehicleDTO { vehicleID = requestDTO.vehicleID} , new clsUserDTO { userID = requestDTO.userID} , new clsRentDTO { startDate = requestDTO.startDate , returnDate = requestDTO.returnDate} , new clsPaymentDTO { paymentMethodID = requestDTO.paymentMethodID });

            return Ok(new clsRentVehicleResponseDTO { isSucceed = isSucceed});
        }


        [HttpPost("Rents/GetUserRents", Name = "GetUserRents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserRents([FromBody] clsGetUserRentsRequestDTO requestDTO)
        {
            if(requestDTO.userID <= 0)
            {
                return BadRequest("Invalid UserID");
            }

            List<clsUserRentDTO> list = clsRent.getUserRents(new clsUserDTO { userID = requestDTO.userID });

            return Ok(list);
        }


        [HttpPost("Information/GetUserInformation", Name = "GetUserInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserInformation([FromBody] clsGetUserInformationRequestDTO requestDTO)
        {

            if(requestDTO.userID <= 0)
            {
                return BadRequest("Invalid UserID");
            }

            clsUser user = clsUser.getUserByUserID(new clsUserDTO { userID = requestDTO.userID });
            if(user == null)
            {            
                return BadRequest("User not found");
            }

            
            clsPerson person = clsPerson.getPersonByPersonID(new clsPersonDTO { personID = user.personID });
            if(person == null)
            {                
                return BadRequest("User not found");
            }


            return Ok(new clsGetUserInformationResponseDTO {  ssn = person.ssn, firstName = person.firstName, secondName = person.secondName, thirdName = person.thirdName, lastName = person.lastName, email = person.email, phoneNumber = person.phoneNumber, gender = (int)person.gender, userRoleID = user.userRoleID, username = user.username });

        }



        [HttpPost("Information/UpdateInformation", Name = "UpdateInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateInformation([FromBody] clsUpdateInformationRequestDTO requestDTO)
        {

            if (requestDTO == null)
            {
                return BadRequest("Request body cannot be empty.");
            }

            if (requestDTO.userID <= 0)
            {
                return BadRequest("Invalid UserID.");
            }

            if (string.IsNullOrWhiteSpace(requestDTO.firstName))
            {
                return BadRequest("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(requestDTO.lastName))
            {
                return BadRequest("Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(requestDTO.email) || !System.Text.RegularExpressions.Regex.IsMatch(requestDTO.email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(requestDTO.phoneNumber) || !System.Text.RegularExpressions.Regex.IsMatch(requestDTO.phoneNumber, @"^\+?\d{10,15}$"))
            {
                return BadRequest("Invalid phone number.");
            }

            if (requestDTO.gender != (int)clsPerson.enGender.Female && requestDTO.gender != (int)clsPerson.enGender.Male)
            {
                return BadRequest("Invalid gender value. Allowed values: 1 (Male), 2 (Female).");
            }

            if (string.IsNullOrWhiteSpace(requestDTO.username) || requestDTO.username.Length < 3)
            {
                return BadRequest("Username must be at least 3 characters long.");
            }

            
            clsUser user = clsUser.getUserByUserID(new clsUserDTO { userID = requestDTO.userID });



            string currentUsername = user.username;            
            if(currentUsername != requestDTO.username && clsUser.doesUsernameExist(new clsUserDTO { username = requestDTO.username }))
            {
                return BadRequest("Username already exist");
            }

            clsPerson person = clsPerson.getPersonByPersonID(new clsPersonDTO { personID = user.personID });

            string currentEmail = person.email;
            if (currentEmail != requestDTO.email && clsPerson.doesEmailExist(new clsPersonDTO { email = requestDTO.email }))
            {
                return BadRequest("Email already exist");
            }

            string currentPhoneNumber = person.phoneNumber;
            if (currentPhoneNumber != requestDTO.phoneNumber && clsPerson.doesPhoneNumberExist(new clsPersonDTO { phoneNumber = requestDTO.phoneNumber }))
            {
                return BadRequest("Phone Number already exist");
            }



            bool isUpdated = clsUser.updateInformationByUserID(new clsUserDTO { userID = requestDTO.userID, username = requestDTO.username }, new clsPersonDTO { firstName = requestDTO.firstName, secondName = requestDTO.secondName, thirdName = requestDTO.thirdName, lastName = requestDTO.lastName, email = requestDTO.email, phoneNumber = requestDTO.phoneNumber, gender = requestDTO.gender });

            return Ok(new clsUpdateInformationResponseDTO { isSucceed = isUpdated});
        }

    }
}
