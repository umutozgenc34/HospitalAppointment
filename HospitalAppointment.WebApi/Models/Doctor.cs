using HospitalAppointment.WebApi.Models.Enums;

namespace HospitalAppointment.WebApi.Models;

public sealed class Doctor : Entity<int>
{
    
    public string Name { get; set; }
    public BranchType Branch { get; set; }

    public List<Appointment> Appointments { get; set; }
}
