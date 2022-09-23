using ArcticaAPI.Models;

namespace ArcticaAPI.DTOModels
{
    public class HospitalDTOWithDoctors: HospitalDTO
    {
        public ICollection<DoctorDTO> Doctors { get; set; }
    }
}
