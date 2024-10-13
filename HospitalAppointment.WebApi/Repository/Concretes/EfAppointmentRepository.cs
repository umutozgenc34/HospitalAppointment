using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.WebApi.Repository.Concretes;

public class EfAppointmentRepository : IAppointmentRepository
{
    private BaseDbContext _context;

    public EfAppointmentRepository(BaseDbContext context)
    {
        _context = context;
    }
    public Appointment Add(Appointment user)
    {
        _context.Appointments.Add(user);
        _context.SaveChanges();
        return user;
    }

    public Appointment Delete(Guid id)
    {
        Appointment app = _context.Appointments.Find(id);
        _context.Appointments.Remove(app);
        _context.SaveChanges();
        return app;
        

    }

    public List<Appointment> GetAll()
    {
        List<Appointment> appointments = _context.Appointments.ToList();
        return appointments;
    }

    public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
    {
        List<Appointment> appointments = _context.Appointments.Where((a => a.DoctorId == doctorId)).ToList();
        return appointments;
    }

    public Appointment? GetById(Guid id)
    {
        Appointment app = _context.Appointments.Find(id);
        return app;
    }

    public Appointment Update(Appointment user)
    {
        _context.Appointments.Update(user);
        _context.SaveChanges();
        return user;
    }
}
