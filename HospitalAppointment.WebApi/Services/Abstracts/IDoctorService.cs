using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Response;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Request;
using HospitalAppointment.WebApi.Models.Enums;
using System.Security.Cryptography;

namespace HospitalAppointment.WebApi.Services.Abstracts;

public interface IDoctorService
{
    Doctor? GetDoctorById(int id);
    List<DoctorResponseDto> GetAllDoctors();

    Doctor AddDoctor(AddDoctorRequestDto requestDto);
    Doctor UpdateDoctor(Doctor user);
    Doctor DeleteDoctor(int id);
    List<Doctor> GetDoctorsByBranch(BranchType branch);
}
