using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer
{
    public class clsManufactureDataAccess
    {

      public static List<clsManufactureDTO> getVehiclesManufactures()
        {
            List<clsManufactureDTO> list = new List<clsManufactureDTO>();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_GetVehiclesManufactures";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                clsManufactureDTO dto = new clsManufactureDTO
                                {
                                    manufactureID = reader.GetInt32(reader.GetOrdinal("ManufactureID")) ,
                                    manufacture = reader.GetString(reader.GetOrdinal("Manufacture")) ,
                                    image = (byte[])reader["Image"]
                                };


                                list.Add(dto);
                            }
                        }
                    }                                  
                }




            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }




            return list;
        }

      public static bool getManufactureByManufactureID(clsManufactureDTO manufactureDTO)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_GetManufactureByManufactureID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"ManufactureID", manufactureDTO.manufactureID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                               manufactureDTO.manufacture = reader.GetString(reader.GetOrdinal("Manufacture"));
                               manufactureDTO.image = reader["Image"] == DBNull.Value ? null : (byte[])reader["Image"];
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

    }
}
