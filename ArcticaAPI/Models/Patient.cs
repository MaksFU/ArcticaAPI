using System.Text.Json.Serialization;

namespace ArcticaAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string TName { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }

        [JsonIgnore]
        public ICollection<Doctor> Doctors { get; set; }
    }
}
