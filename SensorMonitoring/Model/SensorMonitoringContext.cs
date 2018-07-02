using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorMonitoring.Model
{
    public class SensorMonitoringContext : DbContext
    {
        public SensorMonitoringContext(DbContextOptions option) : base(option) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Lab>labs { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }

    }
}
