using HospitalAppointment.WebApi.Exceptions;
using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Appointments.Request;
using HospitalAppointment.WebApi.Models.Dtos.Appointments.Response;
using HospitalAppointment.WebApi.Repository.Abstract;
using HospitalAppointment.WebApi.Services.Abstracts;

namespace HospitalAppointment.WebApi.Services.Concretes
{
    public class AppointmentService : IAppointmentService
    {
        private  IAppointmentRepository _appointmentRepository;
        private IDoctorRepository _doctorRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository , IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
        }

        public Appointment AddAppointment(AddAppointmentRequestDto dto)
        {
            Appointment appointment = (Appointment)dto; // Explicit donusum
            // randevu alınırken hangi doktordan alındığı bilinmelidir kuralı
            var doctor = _doctorRepository.GetById(appointment.DoctorId);
            if (doctor == null)
            {
                throw new NotFoundException("Randevu için geçerli bir doktor seçilmesi gereklidir.");
            }
            //randevu tarihi 3 gün kuralı 
            if (appointment.AppointmentDate < DateTime.Now.AddDays(3))
            {
                throw new ValidationException("Randevu tarihi en az bugünden 3 gün sonra olmalıdır.");
            }
            // doktor ve randevu alınırken isim alanları boş olamaz kuralı
            if (string.IsNullOrWhiteSpace(appointment.PatientName))
            {
                throw new ValidationException("Hasta adı boş olamaz.");
            }

            return _appointmentRepository.Add(appointment);
        }

        public Appointment DeleteAppointment(Guid id)
        {
            return _appointmentRepository.Delete(id);
        }

        public List<AppointmentResponseDto> GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAll();
            return appointments.Select(appointment => (AppointmentResponseDto)appointment).ToList(); // Implicit donusum
        }

        public Appointment? GetAppointmentById(Guid id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                throw new NotFoundException("Randevu bulunamadı."); 
            }
            return appointment;
        }

        public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            return _appointmentRepository.GetAppointmentsByDoctorId(doctorId);
        }

        public Appointment UpdateAppointment(Appointment user)
        {
            // randevu alınırken hangi doktordan alındığı bilinmelidir kuralı
            var doctor = _doctorRepository.GetById(user.DoctorId);
            if (doctor == null)
            {
                throw new NotFoundException("Randevu için geçerli bir doktor seçilmesi gereklidir.");
            }
            //randevu tarihi 3 gün kuralı
            if (user.AppointmentDate < DateTime.Now.AddDays(3))
            {
                throw new ValidationException("Randevu tarihi en az bugünden 3 gün sonra olmalıdır.");
            }

            // doktor ve randevu alınırken isim alanları boş olamaz kuralı
            if (string.IsNullOrWhiteSpace(user.PatientName))
            {
                throw new ValidationException("Hasta adı boş olamaz.");
            }
            // Randevu alınan doktorun mevcut randevu sayısını kontrol et kuralı
            var existingAppointmentsCount = _appointmentRepository.GetAppointmentsByDoctorId(user.DoctorId).Count;
            if (existingAppointmentsCount >= 10)
            {
                throw new InvalidOperationException("Bu doktora ait maksimum randevu sayısına ulaşıldı. (10 randevu)");
            }
            return _appointmentRepository.Update(user);
        }

        public void DeleteExpiredAppointments()
        {
            _appointmentRepository.DeleteExpiredAppointments();
        }
    }
}
