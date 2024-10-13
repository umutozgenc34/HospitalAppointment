using HospitalAppointment.WebApi.Models.Enums;
using HospitalAppointment.WebApi.Models;
using System.Security.Cryptography;

namespace HospitalAppointment.WebApi.Services.Abstracts;

public interface IAppointmentService
{
    Appointment? GetAppointmentById(Guid id);
    List<Appointment> GetAllAppointments();

    Appointment AddAppointment(Appointment user);
    Appointment UpdateAppointment(Appointment user);
    Appointment DeleteAppointment(Guid id);
    List<Appointment> GetAppointmentsByDoctorId(int doctorId);
    void DeleteExpiredAppointments();
}
