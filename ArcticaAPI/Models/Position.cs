using System.Text.Json.Serialization;

namespace ArcticaAPI.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Charge { get; set; }

        [JsonIgnore]
        public ICollection<Doctor> Doctors { get; set; }
    }
}
