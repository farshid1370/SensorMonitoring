using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SensorMonitoring.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Lab> Labs { get; set; }

    }
}
