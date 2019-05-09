using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCarsales.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Type { get; set; }

        [StringLength(50)]
        [Required]
        public string Make { get; set; }

        [StringLength(50)]
        [Required]
        public string  Model { get; set; }

        [StringLength(30)]
        public string Engine { get; set; }
    }
}
