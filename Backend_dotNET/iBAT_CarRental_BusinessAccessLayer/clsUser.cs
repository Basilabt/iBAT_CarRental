using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsUser
    {
        public enum enMode
        {
            AddNew =1 , Update =2, Delete =3,
        }
        
        public int userID {  get; set; }
        public int userRoleID { get; set; }
        public int personID {  get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
        public enMode mode { get; set;}
        public clsUserRole userRole { get; set; }
        public clsPerson person { get; set; }


        public clsUser()
        {
            this.userID = -1;
            this.userRoleID = -1;
            this.personID = - 1;
            this.username = "";
            this.password = "";
            this.isActive = false;
            this.mode = enMode.AddNew;

            //  Get User Role Object
            //  Get Person Object
        }

        private clsUser(int userID , int userRoleID , int personID , string username , string password , Byte isActive)
        {
            this.userID = userID;
            this.userID = userRoleID;
            this.userID = personID;
            this.username = username;
            this.password = password;
            this.mode = enMode.Update;
            this.userRole = null;
            this.person = null;
            this.isActive = true;
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


        public static bool signUp(clsPersonDTO personDTO , clsUserDTO userDTO)
        {
            return clsUserDataAccess.signUp(personDTO, userDTO);
        }

        public static clsUser signInByEmailAndPassword(clsPersonDTO personDTO, clsUserDTO userDTO)
        {
            if (clsUserDataAccess.signInByEmailAndPassword(personDTO, userDTO))
            {
                return new clsUser { userID = userDTO.userID, userRoleID = userDTO.userRoleID, personID = userDTO.personID, username = userDTO.username, password = userDTO.password, isActive = userDTO.isActive };
            }

            return null;
        }

        public static clsUser getUserByEmail(clsPersonDTO personDTO)
        {
            clsUserDTO userDTO  = new clsUserDTO();

            if(clsUserDataAccess.getUserByEmail(userDTO,personDTO))
            {
                return new clsUser { userID = userDTO.userID , userRoleID = userDTO.userRoleID , personID = userDTO.personID , username = userDTO.username , password = userDTO.password , isActive = userDTO.isActive };
            }

            return null;
        }

        public static bool doesUsernameExist(clsUserDTO userDTO)
        {
            return clsUserDataAccess.doesUsernameExist(userDTO);
        }

        public static clsUser getUserByUserID(clsUserDTO userDTO)
        {
            if (clsUserDataAccess.getUserByUserID(userDTO))
            {
                return new clsUser { userID = userDTO.userID, userRoleID = userDTO.userRoleID,personID = userDTO.personID, username = userDTO.username, password = userDTO.password, isActive = userDTO.isActive };
            }

            return null;
        }


        public static bool updateInformationByUserID(clsUserDTO userDTO , clsPersonDTO personDTO)
        {
            return clsUserDataAccess.updateInformationByUserID(userDTO, personDTO);
        }
       

    }
}
