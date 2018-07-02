using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorMonitoring.Model
{
    public class SensorData
    {
        public int Id { get; set; }
        [Required]

        public string Value { get; set; }
        [Required]

        public DateTime Time { get; set; }
        [Required]
        public int Priorty { get; set; }
        [ForeignKey("Sensor")]
        public int SensorId { get; set; }
    }
}
