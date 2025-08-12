using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsUserRole
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public enum enUserRole
        {
            Admin = 1 , User = 2 , Manager = 3 , Support = 4
        }

        public int userRoleID { get; set;}
        public int role {  get; set;}
        public string description { get; set;}

        public enMode mode { get; set;}
        public enUserRole userRole { get; set;}

        public clsUserRole()
        {
            this.userRoleID = -1;
            this.role = -1;
            this.description = "";
        }

        private clsUserRole(int userRoleID , int role , string description)
        {
            this.userRoleID = userRoleID;
            this.role = role;
            this.description = description;
            this.mode = enMode.Update;
            this.userRole = (enUserRole)role;
        }

        public bool save()
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

    }
}
