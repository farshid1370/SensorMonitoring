using Microsoft.EntityFrameworkCore;

namespace SensorMonitoring.Model
{
    public class SensorMonitoringContext : DbContext
    {
        public SensorMonitoringContext(DbContextOptions option) : base(option) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Lab>Labs { get; set; }
        public DbSet<SensorData> SensorDatas { get; set; }

    }
}
