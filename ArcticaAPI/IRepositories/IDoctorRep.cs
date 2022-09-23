using ArcticaAPI.Models;
using System.Numerics;

namespace ArcticaAPI.IRepositories
{
    public interface IDoctorRep
    {
        ICollection<Doctor> GetDoctors(int hospitalId);
        bool UpdateDoctor(Doctor doctor);
    }
}
