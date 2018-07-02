using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMonitoring.Model
{
    public class ResultViewModel
    {
        public bool Validate { get; set; }
        public string ValidateMessage { get; set; }
        public string Message { get; set; }
        public string ExeptionMessage { get; set; }
    }
}
