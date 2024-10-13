using HospitalAppointment.WebApi.Models;

namespace HospitalAppointment.WebApi.Repository.Abstract;

public interface IAppointmentRepository : IRepository<Appointment,Guid>
{
    List<Appointment> GetAppointmentsByDoctorId(int doctorId);
}
