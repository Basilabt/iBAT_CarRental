using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsPerson
    {
        public enum enMode
        {
            AddNew = 1, Update = 2, Delete = 3,
        }

        public enum enGender
        {
            Male = 1 , Female = 2
        }

        public int personID {  get; set; }

        public string ssn { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public enMode mode { get; set; }
        public enGender gender { get; set; }

        public clsPerson()
        {
            this.personID = -1;
            this.ssn = "";
            this.firstName = "";
            this.secondName = "";
            this.thirdName = "";
            this.lastName = "";
            this.email = "";
            this.phoneNumber = "";
            this.mode = enMode.AddNew;
        }

        private clsPerson(int personID, string ssn, string firstName, string secondName, string thirdName, string lastName, string email, string phoneNumber , int gender)
        {
            this.personID = personID;
            this.ssn = ssn;
            this.firstName = firstName;
            this.secondName = secondName;
            this.thirdName = thirdName;
            this.lastName = lastName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.mode = enMode.Update;
            this.gender = (enGender)gender;
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

        public static bool doesSSNExist(clsPersonDTO personDTO)
        {
            return clsPersonDataAccess.doesSSNExist(personDTO);
        }

        public static bool doesEmailExist(clsPersonDTO personDTO)
        {
            return clsPersonDataAccess.doesEmailExist(personDTO);
        }

        public static bool doesPhoneNumberExist(clsPersonDTO personDTO)
        {
            return clsPersonDataAccess.doesPhoneNumberExist(personDTO);
        }

        public static clsPerson getPersonByPersonID(clsPersonDTO personDTO)
        {

            if(clsPersonDataAccess.getPersonByPersonID(personDTO))
            {
               return  new clsPerson { personID = personDTO.personID, ssn = personDTO.ssn, firstName = personDTO.firstName  , secondName = personDTO.secondName , thirdName = personDTO.thirdName , lastName = personDTO.lastName , email = personDTO.email , phoneNumber = personDTO.phoneNumber , gender = (enGender)personDTO.gender};
            }

            return null;
        }
    }
}
