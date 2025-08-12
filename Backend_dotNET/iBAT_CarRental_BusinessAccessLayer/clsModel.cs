using iBAT_CarRental_DataAccessLayer;
using iBAT_CarRental_DataAccessLayer.DTOs;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBAT_CarRental_BusinessAccessLayer
{
    public class clsModel
    {
        public enum enMode
        {
            AddNew = 1 , Update = 2 , Delete = 3 
        }

        public int modelID {  get; set; }
        public string model {  get; set; }
        public enMode mode { get; set; }

        public clsModel()
        {
            this.modelID = -1;
            this.model = "";
            this.mode = enMode.AddNew;
        }

        private clsModel(int modeID, string model)
        {
            this.modelID = modeID;
            this.model = model;
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

        public static clsModel getModelByModelID(clsModelDTO modelDTO)
        {

            if(clsModelDataAccess.getModelByModelID(modelDTO))
            {
                return new clsModel { modelID = modelDTO.modelID , model = modelDTO.model };
            }

            return null;
        }

    }
}
