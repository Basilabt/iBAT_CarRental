using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer
{
    public class clsRentDataAccess
    {
        public static bool rentVehicle(clsVehicleDTO vehicleDTO , clsUserDTO userDTO , clsRentDTO rentDTO , clsPaymentDTO paymentDTO)
        {
            int numberOfAffectedRows = 0;

            try
            {

                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_AddNewRent";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(@"VehicleID",vehicleDTO.vehicleID);
                        command.Parameters.AddWithValue(@"UserID",userDTO.userID);
                        command.Parameters.AddWithValue(@"StartDate",rentDTO.startDate);
                        command.Parameters.AddWithValue(@"ReturnDate", rentDTO.returnDate);
                        command.Parameters.AddWithValue(@"PaymentMethodID", paymentDTO.paymentMethodID);

                        numberOfAffectedRows = command.ExecuteNonQuery();
                    }
                }
            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return numberOfAffectedRows >= 1;
        }

        public static List<clsUserRentDTO> getUserRents(clsUserDTO userDTO)
        {
            List<clsUserRentDTO> list = new List<clsUserRentDTO>();

            try
            {

                using(SqlConnection connection = new SqlConnection((clsDataAccessSettings.connectionString)))
                {
                    connection.Open();

                    string cmd = "SP_GetUserRents";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"UserID",userDTO.userID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {                            
                            while(reader.Read())
                            {
                                clsUserRentDTO dto = new clsUserRentDTO();

                                dto.rentID = reader.GetInt32(reader.GetOrdinal("RentID"));

                                int startDateIndex = reader.GetOrdinal("StartDate");
                                if (!reader.IsDBNull(startDateIndex))
                                {
                                    DateTime dateTime = reader.GetDateTime(startDateIndex);
                                    dto.startDate = DateOnly.FromDateTime(dateTime);
                                }


                                int returnDateIndex = reader.GetOrdinal("ReturnDate");
                                if (!reader.IsDBNull(returnDateIndex))
                                {
                                    DateTime dateTime = reader.GetDateTime(returnDateIndex);
                                    dto.returnDate = DateOnly.FromDateTime(dateTime);
                                }

                                dto.startMileage = reader.GetInt32(reader.GetOrdinal("StartMileage"));
                                dto.notes = reader.GetString(reader.GetOrdinal("Notes"));
                                dto.vehicleID = reader.GetInt32(reader.GetOrdinal("VehicleID"));
                                dto.plateNumber = reader.GetString(reader.GetOrdinal("PlateNumber"));
                                dto.dailyRate = reader.GetDecimal(reader.GetOrdinal("DailyRate"));
                                dto.image = reader["Image"] != DBNull.Value ? (byte[])reader["Image"] : null;
                                dto.manufacture = reader.GetString(reader.GetOrdinal("Manufacture"));
                                dto.model = reader.GetString(reader.GetOrdinal("Model"));
                                dto.fuelType = reader.GetString(reader.GetOrdinal("FuelType"));
                                dto.transmissionType = reader.GetString(reader.GetOrdinal("TransmissionType"));


                                list.Add(dto);

                            }
                        }
                    }
                }


            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return list;
        }


    }
}
