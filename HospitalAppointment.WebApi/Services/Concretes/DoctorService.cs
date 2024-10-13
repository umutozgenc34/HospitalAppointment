using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Response;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Request;
using HospitalAppointment.WebApi.Models.Enums;
using HospitalAppointment.WebApi.Repository.Abstract;
using HospitalAppointment.WebApi.Services.Abstracts;
using System.Numerics;
using HospitalAppointment.WebApi.Models.ReturnModels;

namespace HospitalAppointment.WebApi.Services.Concretes;

public class DoctorService : IDoctorService
{
    private IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }
    public Doctor AddDoctor(AddDoctorRequestDto requestDto)
    {
        Doctor doctor = (Doctor)requestDto; // Explicit donusum
        // doktor ve randevu alınırken isim alanları boş olamaz kuralı
        if (string.IsNullOrWhiteSpace(requestDto.Name))
        {
            throw new ArgumentException("Doktor ismi boş bırakılamaz.");
        }
        return _doctorRepository.Add(doctor);
    }

    public Doctor DeleteDoctor(int id)
    {
        return _doctorRepository.Delete(id);
    }

    // return model kullanımı
    public ReturnModel<List<DoctorResponseDto>> GetAll()
    {
        try
        {
            var doctors = _doctorRepository.GetAll();
            return new ReturnModel<List<DoctorResponseDto>>(true, "Doktorlar başarıyla getirildi.", doctors.Select(d => (DoctorResponseDto)d).ToList());
        }
        catch (Exception ex)
        {
            return new ReturnModel<List<DoctorResponseDto>>(false, $"Bir hata oluştu: {ex.Message}", null);
        }
    }

    public Doctor? GetDoctorById(int id)
    {
        return _doctorRepository.GetById(id);
    }

    public List<Doctor> GetDoctorsByBranch(BranchType branch)
    {
        return _doctorRepository.GetDoctorsByBranch(branch);
    }

    public Doctor UpdateDoctor(Doctor user)
    {
        // doktor ve randevu alınırken isim alanları boş olamaz kuralı
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("Doktor ismi boş bırakılamaz.");
        }
        return _doctorRepository.Update(user);
    }
}
