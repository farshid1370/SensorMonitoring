using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorMonitoring.Model
{
    public class Sensor
    {
        public int Id { get; set; }
        [Required]
        public int IndicatorCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [ForeignKey("Lab")]
        public int LabId { get; set; }
        public ICollection<SensorData> SensorDatas { get; set; }
    }
}
