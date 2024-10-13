using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Enums;
using HospitalAppointment.WebApi.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.WebApi.Repository.Concretes
{
    public class EfDoctorRepository : IDoctorRepository
    {
        private BaseDbContext _context;

        public EfDoctorRepository(BaseDbContext context)
        {
            _context = context;
        }

        public Doctor Add(Doctor user)
        {
            _context.Doctors.Add(user);
            _context.SaveChanges();

            return user;
        }

        public Doctor Delete(int id)
        {
            Doctor doctor = _context.Doctors.Find(id);
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return doctor;

        }

        public List<Doctor> GetAll()
        {
            List<Doctor> doctors = _context.Doctors.ToList();
            return doctors;
        }

        public Doctor? GetById(int id)
        {
            Doctor doctor = _context.Doctors.Find(id);

            return doctor;
        }

        public List<Doctor> GetDoctorsByBranch(BranchType branch)
        {
            List<Doctor> doctors = _context.Doctors.Where(d => d.Branch == branch).ToList();
            return doctors;
        }

        public Doctor Update(Doctor user)
        {
            _context.Doctors.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
