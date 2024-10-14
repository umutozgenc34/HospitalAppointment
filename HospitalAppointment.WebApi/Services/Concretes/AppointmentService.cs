using HospitalAppointment.WebApi.Exceptions;
using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Appointments.Request;
using HospitalAppointment.WebApi.Models.Dtos.Appointments.Response;
using HospitalAppointment.WebApi.Repository.Abstract;
using HospitalAppointment.WebApi.Services.Abstracts;
using System;

namespace HospitalAppointment.WebApi.Services.Concretes
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
        }

        public Appointment AddAppointment(AddAppointmentRequestDto dto)
        {
            try
            {
                Appointment appointment = (Appointment)dto; // Explicit dönüşüm

                ValidateDoctorExists(appointment.DoctorId); // Doktor kontrolü
                ValidateAppointmentDate(appointment.AppointmentDate); // Randevu tarihi kontrolü
                ValidatePatientName(appointment.PatientName); // Hasta adı kontrolü
                ValidateDoctorAppointmentLimit(appointment.DoctorId); // Randevu sayısı kontrolü

                return _appointmentRepository.Add(appointment);
            }
            catch (NotFoundException ex)
            {
                // Doktor bulunamazsa
                throw new NotFoundException(ex.Message);
            }
            catch (ValidationException ex)
            {
                // Validasyon hatası
                throw new ValidationException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // İşlem sırasında bir hata oluşursa
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                // Beklenmedik hatalar
                throw new Exception($"Bir hata oluştu: {ex.Message}");
            }
        }

        public Appointment DeleteAppointment(Guid id)
        {
            try
            {
                return _appointmentRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu silinirken bir hata oluştu: {ex.Message}");
            }
        }

        public List<AppointmentResponseDto> GetAllAppointments()
        {
            try
            {
                var appointments = _appointmentRepository.GetAll();
                return appointments.Select(appointment => (AppointmentResponseDto)appointment).ToList(); // Implicit dönüşüm
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevular getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public Appointment? GetAppointmentById(Guid id)
        {
            try
            {
                var appointment = _appointmentRepository.GetById(id);
                if (appointment == null)
                {
                    throw new NotFoundException("Randevu bulunamadı.");
                }
                return appointment;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                return _appointmentRepository.GetAppointmentsByDoctorId(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Doktora ait randevular getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public Appointment UpdateAppointment(Appointment user)
        {
            try
            {
                ValidateDoctorExists(user.DoctorId); // Doktor kontrolü
                ValidateAppointmentDate(user.AppointmentDate); // Randevu tarihi kontrolü
                ValidatePatientName(user.PatientName); // Hasta adı kontrolü
                ValidateDoctorAppointmentLimit(user.DoctorId); // Randevu sayısı kontrolü
                return _appointmentRepository.Update(user);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Randevu güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        //tarihi geçmiş randevuları sil
        public void DeleteExpiredAppointments()
        {
            try
            {
                _appointmentRepository.DeleteExpiredAppointments();
            }
            catch (Exception ex)
            {
                throw new Exception($"Süresi dolmuş randevular silinirken bir hata oluştu: {ex.Message}");
            }
        }

        //validation methods
        // randevu için doktor seçilmeli kuralı
        private void ValidateDoctorExists(int doctorId)
        {
            var doctor = _doctorRepository.GetById(doctorId);
            if (doctor == null)
            {
                throw new NotFoundException("Randevu için geçerli bir doktor seçilmesi gereklidir.");
            }
        }
        // randevu tarihi en az 3 gün sonra olmali kurali
        private void ValidateAppointmentDate(DateTime appointmentDate)
        {
            if (appointmentDate < DateTime.Now.AddDays(3))
            {
                throw new ValidationException("Randevu tarihi en az bugünden 3 gün sonra olmalıdır.");
            }
        }
        // hasta adı boş olmamalı
        private void ValidatePatientName(string patientName)
        {
            if (string.IsNullOrWhiteSpace(patientName))
            {
                throw new ValidationException("Hasta adı boş olamaz.");
            }
        }

        // bir doktor maksimum 10 hasta alabilir
        private void ValidateDoctorAppointmentLimit(int doctorId)
        {
            var existingAppointmentsCount = _appointmentRepository.GetAppointmentsByDoctorId(doctorId).Count;
            if (existingAppointmentsCount >= 10)
            {
                throw new InvalidOperationException("Bu doktora ait maksimum randevu sayısına ulaşıldı. (10 randevu)");
            }
        }
    }
}

