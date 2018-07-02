using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMonitoring.Model
{
    public class Sensor
    {
        public int ID { get; set; }
        [Required]
        public int IndicatorCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [ForeignKey("Lab")]
        public int LabID { get; set; }
        public ICollection<SensorData> SensorDatas { get; set; }
    }
}
