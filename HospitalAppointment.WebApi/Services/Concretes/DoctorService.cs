using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Response;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Request;
using HospitalAppointment.WebApi.Models.Enums;
using HospitalAppointment.WebApi.Repository.Abstract;
using HospitalAppointment.WebApi.Services.Abstracts;
using HospitalAppointment.WebApi.Models.ReturnModels;
using HospitalAppointment.WebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalAppointment.WebApi.Services.Concretes
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Doctor AddDoctor(AddDoctorRequestDto requestDto)
        {
            try
            {
                Doctor doctor = (Doctor)requestDto; // Explicit dönüşüm
                ValidateDoctorName(requestDto.Name); // Doktor ismi boş olamaz
                return _doctorRepository.Add(doctor);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktor eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        public Doctor DeleteDoctor(int id)
        {
            try
            {
                return _doctorRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktor silinirken bir hata oluştu: {ex.Message}");
            }
        }

        // ReturnModel kullanımı
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
            try
            {
                var doctor = _doctorRepository.GetById(id);
                if (doctor == null)
                {
                    throw new NotFoundException("Doktor bulunamadı.");
                }
                return doctor;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktor getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public List<Doctor> GetDoctorsByBranch(BranchType branch)
        {
            try
            {
                return _doctorRepository.GetDoctorsByBranch(branch);
            }
            catch (Exception ex)
            {
                throw new Exception($"Branşa ait doktorlar getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public Doctor UpdateDoctor(Doctor user)
        {
            try
            {
                ValidateDoctorName(user.Name); // Doktor ismi boş olamaz
                return _doctorRepository.Update(user);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktor güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        // Validation methods
        // Doktor ve randevu alınırken isim alanları boş olamaz kuralı
        private void ValidateDoctorName(string doctorName)
        {
            if (string.IsNullOrWhiteSpace(doctorName))
            {
                throw new ValidationException("Doktor ismi boş bırakılamaz.");
            }
        }
    }
}
