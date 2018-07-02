using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMonitoring.Model
{
    public class SensorData
    {
        public int ID { get; set; }
        [Required]

        public string Value { get; set; }
        [Required]

        public DateTime Time { get; set; }
        [Required]
        public int Priorty { get; set; }
        [ForeignKey("Sensor")]
        public int SensorID { get; set; }
    }
}
