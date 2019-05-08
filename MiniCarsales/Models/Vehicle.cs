using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCarsales.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string  Model { get; set; }
        public string Engine { get; set; }
    }
}
