using ArcticaAPI.DTOModels;
using ArcticaAPI.IRepositories;
using ArcticaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArcticaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly IHospitalRep _hospitalRep;
        private readonly IDoctorRep _doctorRep;
        private readonly IPositionRep _positionRep;

        public HospitalController(IHospitalRep hospitalRep, IDoctorRep doctorRep, IPositionRep positionRep)
        {
            _hospitalRep = hospitalRep;
            _doctorRep = doctorRep;
            _positionRep = positionRep;
        }


        [HttpGet("/api/GetHospitals")]
        public IActionResult GetHospitals()
        {
            var hospitals = _hospitalRep.GetHospitals();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(hospitals);
        }


        [HttpGet("/api/GetHospitalById{hospitalId}")]
        public IActionResult GetHospital(int hospitalId)
        {
            if (!_hospitalRep.HospitalExists(hospitalId))
                return NotFound($"'Hospital with Id {hospitalId} NotFound'");

            var hospital = _hospitalRep.GetHospitalById(hospitalId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(hospital);
        }


        [HttpGet("/api/GetHospitalsWithDoctors")]
        public IActionResult GetHospitalsWithDoctors()
        {
            var hospitals = _hospitalRep.GetHospitalsWithDoctors();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(hospitals);
        }


        [HttpGet("/api/GetHospitalWithDoctorsById{hospitalId}")]
        public IActionResult GetHospitalWithDoctorsById(int hospitalId)
        {
            if (!_hospitalRep.HospitalExists(hospitalId))
                return NotFound($"'Hospital with Id {hospitalId} NotFound'");

            var hospital = _hospitalRep.GetHospitalWithDoctorsById(hospitalId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(hospital);
        }


        [HttpPost("/api/CreateHospital")]
        public IActionResult CreateHospital([FromBody] HospitalDTO newHospital)
        {
            if (newHospital == null)
                return BadRequest(ModelState);

            var hospital = _hospitalRep.GetHospitalsWithDoctors()
                .Where(h => h.Name.Trim().ToUpper() == newHospital.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (hospital != null)
            {
                ModelState.AddModelError("", "Hospital already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var finalHospital = new Hospital
            {
                Name = newHospital.Name,
                Address = newHospital.Address,
                CarPark = newHospital.CarPark,
            };

            if (!_hospitalRep.CreateHospital(finalHospital))
            {
                ModelState.AddModelError("", "Error during saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPost("/api/CreateHospitalWithNewDoctors")]
        public IActionResult CreateHospitalWithDoctors([FromBody] HospitalDTOWithDoctors newHospital, int positionId)
        {
            if (newHospital == null)
                return BadRequest(ModelState);

            var hospital = _hospitalRep.GetHospitalsWithDoctors()
                .Where(h => h.Name.Trim().ToUpper() == newHospital.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (hospital != null)
            {
                ModelState.AddModelError("", "Hospital already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_positionRep.PositionExists(positionId))
                return NotFound($"'Position with Id {positionId} NotFound'");

            Doctor[] doctors = new Doctor[newHospital.Doctors.Count];
            var count = 0;
            foreach (DoctorDTO d in newHospital.Doctors)
            {
                doctors[count] = new Doctor { FName = d.FName, SName = d.SName, TName = d.TName, Age = d.Age, PositionId = positionId };
                count++;
            }

            var finalHospital = new Hospital
            {
                Name = newHospital.Name,
                Address = newHospital.Address,
                CarPark = newHospital.CarPark,
                Doctors = doctors
            };

            if (!_hospitalRep.CreateHospital(finalHospital))
            {
                ModelState.AddModelError("", "Error during saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("/api/UpdateHospital{hospitalId}")]
        public IActionResult UpdateHospital(int hospitalId, [FromBody] HospitalDTO updHospital)
        {
            if (updHospital == null)
                return BadRequest(ModelState);

            if (!_hospitalRep.HospitalExists(hospitalId))
                return NotFound($"'Hospital with Id {hospitalId} NotFound'");

            var finalHospital = new Hospital { Id = hospitalId, Name = updHospital.Name, Address = updHospital.Address, CarPark = updHospital.CarPark };

            if (!_hospitalRep.UpdateHospital(finalHospital))
            {
                ModelState.AddModelError("", "Error during saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }


        [HttpDelete("/api/DeleteHospital{hospitalId}")]
        public IActionResult UpdateHospital(int hospitalId, [FromQuery] int newHospitalId)
        {
            if (!_hospitalRep.HospitalExists(newHospitalId))
                return NotFound($"'Hospital with Id {newHospitalId} NotFound'");

            if (!_hospitalRep.HospitalExists(hospitalId))
                return NotFound($"'Hospital with Id {hospitalId} NotFound'");

            foreach (var doctor in _doctorRep.GetDoctors(hospitalId))
            {
                doctor.HospitalId = newHospitalId;
                _doctorRep.UpdateDoctor(doctor);
            }

            var finalHospital = _hospitalRep.GetHospitalById(hospitalId);
        
            if (!_hospitalRep.DeleteHospital(finalHospital))
            {
                ModelState.AddModelError("", "Error during saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
