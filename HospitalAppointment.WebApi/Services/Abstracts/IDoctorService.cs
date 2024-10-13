using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Enums;
using System.Security.Cryptography;

namespace HospitalAppointment.WebApi.Services.Abstracts;

public interface IDoctorService
{
    Doctor? GetDoctorById(int id);
    List<Doctor> GetAllDoctors();

    Doctor AddDoctor(Doctor user);
    Doctor UpdateDoctor(Doctor user);
    Doctor DeleteDoctor(int id);
    List<Doctor> GetDoctorsByBranch(BranchType branch);
}
