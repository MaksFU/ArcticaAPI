using ArcticaAPI.DTOModels;
using ArcticaAPI.Models;

namespace ArcticaAPI.IRepositories
{
    public interface IHospitalRep
    {
        ICollection<Hospital> GetHospitals();
        Hospital GetHospitalById(int id);
        ICollection<Hospital> GetHospitalsWithDoctors();
        Hospital GetHospitalWithDoctorsById(int id);
        bool CreateHospital(Hospital hospital);
        bool UpdateHospital(Hospital hospital);
        bool DeleteHospital(Hospital hospital);
        bool HospitalExists(int id);
    }
}
