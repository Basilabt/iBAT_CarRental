using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data;
using Microsoft.Data.SqlClient;


namespace iBAT_CarRental_DataAccessLayer
{
    public class clsUserDataAccess
    {

        public static bool signUp(clsPersonDTO personDTO, clsUserDTO userDTO)
        {
            bool isSucceed = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_SignUp";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(@"SSN", personDTO.ssn);
                        command.Parameters.AddWithValue(@"FirstName", personDTO.firstName);
                        command.Parameters.AddWithValue(@"SecondName", personDTO.secondName);
                        command.Parameters.AddWithValue(@"ThirdName", personDTO.thirdName);
                        command.Parameters.AddWithValue(@"LastName", personDTO.lastName);
                        command.Parameters.AddWithValue(@"Email", personDTO.email);
                        command.Parameters.AddWithValue(@"PhoneNumber", personDTO.phoneNumber);
                        command.Parameters.AddWithValue(@"Gender", personDTO.gender);

                        command.Parameters.AddWithValue(@"Username", userDTO.username);
                        command.Parameters.AddWithValue(@"Password", userDTO.password);

                        using (SqlDataReader readr = command.ExecuteReader())
                        {
                            if (readr.HasRows)
                            {
                                isSucceed = true;
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isSucceed;
        }

        public static bool signInByEmailAndPassword(clsPersonDTO personDTO , clsUserDTO userDTO)
        {
            bool isSignInSucceed = false;


            try
            {

                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_SignInByEmailAndPassword";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Email",personDTO.email);
                        command.Parameters.AddWithValue(@"Password", userDTO.password);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isSignInSucceed = true;

                                userDTO.userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                userDTO.personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                                userDTO.userRoleID = reader.GetInt32(reader.GetOrdinal("UserRoleID"));
                                userDTO.username = reader.GetString(reader.GetOrdinal("Username"));
                                userDTO.password = reader.GetString(reader.GetOrdinal("Password"));
                                userDTO.isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            }
                        }
                    }
                }
                 

            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return isSignInSucceed;
        }

        public static bool getUserByEmail(clsUserDTO userDTO , clsPersonDTO personDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_GetUserByEmail";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Email",personDTO.email);


                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isFound = true;

                                userDTO.userID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                userDTO.personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                                userDTO.userRoleID = reader.GetInt32(reader.GetOrdinal("UserRoleID"));
                                userDTO.username = reader.GetString(reader.GetString(reader.GetOrdinal("Username")));
                                userDTO.password = reader.GetString(reader.GetString("Password"));
                                userDTO.isActive = reader.GetBoolean(reader.GetString("IsActive"));

                            }
                        }
                    }
                }
                 
            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }

        public static bool doesUsernameExist(clsUserDTO userDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_DoesUsernameExist";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Username",userDTO.username);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                isFound = true;
                            }
                        }
                    }
                }


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }
     
        public static bool getUserByUserID(clsUserDTO userDTO)
        {
            bool isFound = false;

            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string cmd = "SP_GetUserByUserID";
                using(SqlCommand command = new SqlCommand(cmd,connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(@"UserID",userDTO.userID);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            isFound = true;


                            userDTO.personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                            userDTO.username = reader.GetString(reader.GetOrdinal("Username"));
                            userDTO.password = reader.GetString(reader.GetOrdinal("Password"));
                            userDTO.userRoleID = reader.GetInt32(reader.GetOrdinal("UserRoleID"));
                            userDTO.isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                        }
                    }

                }



            }

            return isFound;
        }
        
        public static bool updateInformationByUserID(clsUserDTO userDTO , clsPersonDTO personDTO)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_EditInformationByUserID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"UserID",userDTO.userID);
                        command.Parameters.AddWithValue(@"Username", userDTO.username);
                        command.Parameters.AddWithValue(@"FirstName", personDTO.firstName);
                        command.Parameters.AddWithValue(@"SecondName", personDTO.secondName);
                        command.Parameters.AddWithValue(@"ThirdName", personDTO.thirdName);
                        command.Parameters.AddWithValue(@"LastName", personDTO.lastName);
                        command.Parameters.AddWithValue(@"Email", personDTO.email);
                        command.Parameters.AddWithValue(@"PhoneNumber", personDTO.phoneNumber);
                        command.Parameters.AddWithValue(@"Gender", personDTO.gender);





                        numberOfAffectedRows = command.ExecuteNonQuery();                        
                    }
                }

            }
            catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }




            return numberOfAffectedRows > 0;
        }

    }
}
