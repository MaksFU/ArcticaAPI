using ArcticaAPI.IRepositories;
using ArcticaAPI.Models;

namespace ArcticaAPI.Repositories
{
    public class DoctorRep : IDoctorRep
    {
        private readonly MyDbContext _db;

        public DoctorRep(MyDbContext myDbContext)
        {
            _db = myDbContext;
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            _db.Update(doctor);
            return _db.SaveChanges() > 0;
        }

        public ICollection<Doctor> GetDoctors(int hospitalId)
        {
            return _db.Doctors.Where(d => d.HospitalId == hospitalId).ToList();
        }
    }
}
