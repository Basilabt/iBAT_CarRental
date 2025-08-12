using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer
{
    public class clsVehicleDataAccess
    {
        public static List<clsVehicleDTO> getAvailableVehicles()
        {
            List<clsVehicleDTO> list = new List<clsVehicleDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_GetAvailableVehicles";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsVehicleDTO vehicle = new clsVehicleDTO
                                {
                                    vehicleID = (int)reader["VehicleID"],
                                    manufactureID = (int)reader["ManufactureID"],
                                    modelID = (int)reader["ModelID"],
                                    fuelTypeID = (int)reader["FuelTypeID"],
                                    transmissionTypeID = (int)reader["TransmissionTypeID"],
                                    vin = reader["VIN"].ToString(),
                                    plateNumber = reader["PlateNumber"].ToString(),
                                    year = (int)reader["Year"],
                                    seats = (int)reader["Seats"],
                                    color = reader["Color"].ToString(),
                                    dailyRate = (decimal)reader["DailyRate"],
                                    isAvailable = (bool)reader["IsAvailable"],
                                    image = reader["Image"] == DBNull.Value ? null : (byte[])reader["Image"],
                                    mileage = (int)reader["Mileage"]
                                };

                                list.Add(vehicle);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return list;
        }



    }
}
