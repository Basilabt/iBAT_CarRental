using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_DataAccessLayer
{
    public class clsTransmissionTypeDataAccess
    {
        public static bool getTransmissionTypeByTransmissionTypeID(clsTransmissionTypeDTO transmissionTypeDTO)
        {
            bool isFound = false;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_GetTransmissionTypeByTransmissionTypeID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(@"TransmissionTypeID",transmissionTypeDTO.transmissionTypeID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isFound = true;

                                transmissionTypeDTO.type = reader.GetInt32(reader.GetOrdinal("Type"));
                                transmissionTypeDTO.description = reader.GetString(reader.GetOrdinal("Description"));
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
