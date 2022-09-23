using System.Text.Json.Serialization;

namespace ArcticaAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string TName { get; set; }
        public int Age { get; set; }
        public ICollection<Patient> Patients { get; set; }

        [JsonIgnore]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [JsonIgnore]
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
