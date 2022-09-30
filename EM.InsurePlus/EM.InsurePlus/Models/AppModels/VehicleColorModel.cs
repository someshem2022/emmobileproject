using System;
using System.Collections.Generic;
using System.Text;

namespace EM.InsurePlus.Models.AppModels
{
    public class VehicleColorModel
    {
        public string Name { get; set; }

        public string Order { get; set; }
    }

    public class VehicleColorObject
    {
        public List<VehicleColorModel> colors { get; set; }

    }
}
