using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer
{
    public class clsPersonDataAccess
    {

        public static bool doesEmailExist(clsPersonDTO personDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_DoesEmailExist";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"Email", personDTO.email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isFound = true;
                            }
                        }
                    }
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }

        public static bool doesPhoneNumberExist(clsPersonDTO personDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_DoesPhoneNumberExist";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"PhoneNumber", personDTO.phoneNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isFound = true;
                            }
                        }
                    }
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }
     
        public static bool doesSSNExist(clsPersonDTO personDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_DoesSSNExist";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"SSN", personDTO.ssn);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isFound = true;
                            }
                        }
                    }
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }

        public static bool getPersonByPersonID(clsPersonDTO personDTO)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                connection.Open();

                string cmd = "SP_GetPersonByPersonID";
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(@"PersonID", personDTO.personID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            personDTO.personID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                            personDTO.ssn = reader.GetString(reader.GetOrdinal("SSN"));
                            personDTO.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            personDTO.secondName = reader.GetString(reader.GetOrdinal("SecondName"));
                            personDTO.thirdName = reader.GetString(reader.GetOrdinal("ThirdName"));
                            personDTO.lastName = reader.GetString(reader.GetOrdinal("LastName"));
                            personDTO.email = reader.GetString(reader.GetOrdinal("Email"));
                            personDTO.phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                            personDTO.gender = Convert.ToInt32(reader["Gender"]);

                        }
                    }

                }



            }

            return isFound;

        }
    }
}
