using System;
using System.Collections.Generic;
using System.Text;

namespace EM.InsurePlus.Models.AppModels
{
    public class VehicleMakeModel
    {
        public string Name { get; set; }    

        public string Order { get; set; }
    }

    public class VehicleRootObject
    {
        public List<VehicleMakeModel> makes { get; set; }

    }
}

