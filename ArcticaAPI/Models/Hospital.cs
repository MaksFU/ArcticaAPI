namespace ArcticaAPI.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool CarPark { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
