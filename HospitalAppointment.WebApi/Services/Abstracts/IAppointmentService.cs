using HospitalAppointment.WebApi.Models.Enums;
using HospitalAppointment.WebApi.Models;
using System.Security.Cryptography;
using HospitalAppointment.WebApi.Models.Dtos.Appointments.Request;
using HospitalAppointment.WebApi.Models.Dtos.Appointments.Response;

namespace HospitalAppointment.WebApi.Services.Abstracts;

public interface IAppointmentService
{
    Appointment? GetAppointmentById(Guid id);
    List<AppointmentResponseDto> GetAllAppointments();

    Appointment AddAppointment(AddAppointmentRequestDto dto);
    Appointment UpdateAppointment(Appointment user);
    Appointment DeleteAppointment(Guid id);
    List<Appointment> GetAppointmentsByDoctorId(int doctorId);
    void DeleteExpiredAppointments();
}
