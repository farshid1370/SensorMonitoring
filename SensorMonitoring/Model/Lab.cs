using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorMonitoring.Model
{
    public class Lab
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public ICollection<Sensor> Sensors { get; set; }

    }
}
