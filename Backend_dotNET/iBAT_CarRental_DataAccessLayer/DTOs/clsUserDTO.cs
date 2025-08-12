using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer.DTOs
{
    public class clsUserDTO
    {
        public int userID {  get; set; }
        public int userRoleID { get; set; }
        public int personID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
    }
}
