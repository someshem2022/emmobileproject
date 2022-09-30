using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.InsurePlus.Services.Models
{
    public class VehiclePolicy
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nic { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string Year { get; set; }

        public string LicencePlate { get; set; }

        public string Image { get; set; }

        public DateTime DateTime { get; set; }
    }
}
