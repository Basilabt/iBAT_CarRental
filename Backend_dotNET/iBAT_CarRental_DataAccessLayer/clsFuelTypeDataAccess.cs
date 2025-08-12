using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer
{
    public class clsFuelTypeDataAccess
    {
        public static bool getFuelTypeByFuelTypeID(clsFuelTypeDTO fuelTypeDTO)
        {
            bool isFound = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_GetFuelTypeByFuelTypeID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"FuelTypeID",fuelTypeDTO.fuelTypeID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isFound = true;

                                fuelTypeDTO.type = reader.GetInt32(reader.GetOrdinal("Type"));
                                fuelTypeDTO.description = reader.GetString(reader.GetOrdinal("Description"));
                            }  

                        }
                    }
                }

            } catch(Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");

            }


            return isFound;
        }



    }
}
