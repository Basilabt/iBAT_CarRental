using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsTransmissionType
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3
        }

        public int transmissionTypeID {  get; set; }
        public int type {  get; set; }
        public string description { get; set; }
        public enMode mode { get; set; }

        public clsTransmissionType()
        {
            this.transmissionTypeID = -1;
            this.type = -1;
            this.description = "";
            this.mode = enMode.AddNew;
        }

        private clsTransmissionType(int transmissionTypeID, int type, string description)
        {
            this.transmissionTypeID = transmissionTypeID;
            this.type = type;
            this.description = description;
            this.mode = enMode.Update;
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

        public static clsTransmissionType getTransmissionTypeByTransmissionTypeID(clsTransmissionTypeDTO transmissionTypeDTO)
        {
            if (clsTransmissionTypeDataAccess.getTransmissionTypeByTransmissionTypeID(transmissionTypeDTO))
            {
                return new clsTransmissionType { transmissionTypeID = transmissionTypeDTO.transmissionTypeID, type = transmissionTypeDTO.type, description = transmissionTypeDTO.description };
            }

            return null;
        }
    }
}
