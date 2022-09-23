using ArcticaAPI.DTOModels;
using ArcticaAPI.IRepositories;
using ArcticaAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ArcticaAPI.Repositories
{
    public class HospitalRep : IHospitalRep
    {
        private readonly MyDbContext _db;

        public HospitalRep(MyDbContext myDbContext)
        {
            _db = myDbContext;
        }

        public ICollection<Hospital> GetHospitals()
        {
            return _db.Hospitals.ToList();
        }

        public Hospital GetHospitalById(int id)
        {
            return _db.Hospitals.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<Hospital> GetHospitalsWithDoctors()
        {
            return _db.Hospitals
                .Include(h => h.Doctors).ThenInclude(d => d.Patients)
                .Include(h => h.Doctors).ThenInclude(d => d.Position)
                .ToList();
        }
        public Hospital GetHospitalWithDoctorsById(int id)
        {
            return _db.Hospitals
                .Include(h => h.Doctors).ThenInclude(d => d.Patients)
                .Include(h => h.Doctors).ThenInclude(d => d.Position)
                .Where(t => t.Id == id).FirstOrDefault();
        }

        public bool HospitalExists(int id)
        {
            return _db.Hospitals.Any(h => h.Id == id);
        }

        public bool CreateHospital(Hospital hospital)
        {
            _db.Add(hospital);
            return _db.SaveChanges() > 0;
        }
        public bool UpdateHospital(Hospital hospital)
        {
            _db.Update(hospital);
            return _db.SaveChanges() > 0;
        }

        public bool DeleteHospital(Hospital hospital)
        {
            _db.Remove(hospital);
            return _db.SaveChanges() > 0;
        }
    }
}
