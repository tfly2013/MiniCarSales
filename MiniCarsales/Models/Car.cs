using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCarsales.Models
{
    public class Car : Vehicle
    {
        public int Doors { get; set; }
        public int Wheels { get; set; }
        public string BodyType { get; set; }
    }
}
