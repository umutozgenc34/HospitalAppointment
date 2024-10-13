using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Response;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Request;
using HospitalAppointment.WebApi.Models.Enums;
using System.Security.Cryptography;
using HospitalAppointment.WebApi.Models.ReturnModels;

namespace HospitalAppointment.WebApi.Services.Abstracts;

public interface IDoctorService
{
    Doctor? GetDoctorById(int id);
    ReturnModel<List<DoctorResponseDto>> GetAll();

    Doctor AddDoctor(AddDoctorRequestDto requestDto);
    Doctor UpdateDoctor(Doctor user);
    Doctor DeleteDoctor(int id);
    List<Doctor> GetDoctorsByBranch(BranchType branch);
}
