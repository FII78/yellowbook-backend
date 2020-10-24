using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Backend.Models.Location
{
    public class LocationVM
    {
        
           
            public string Name { get; set; }
            public string Description { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        
    }
}
