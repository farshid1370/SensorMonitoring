using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMonitoring.Model
{
    public class Lab
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public ICollection<Sensor> Sensors { get; set; }

    }
}
