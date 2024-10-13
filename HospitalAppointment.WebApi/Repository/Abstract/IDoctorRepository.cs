using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Enums;

namespace HospitalAppointment.WebApi.Repository.Abstract;

public interface IDoctorRepository : IRepository<Doctor,int>
{
    List<Doctor> GetDoctorsByBranch(BranchType branch);
}
