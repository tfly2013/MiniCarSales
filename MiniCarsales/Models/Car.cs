using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCarsales.Models
{
    public class Car : Vehicle
    {
        [Range(0, 10)]
        public int Doors { get; set; }

        [Range(0, 10)]
        public int Wheels { get; set; }

        [StringLength(30)]
        [Required]
        public string BodyType { get; set; }
    }
}
