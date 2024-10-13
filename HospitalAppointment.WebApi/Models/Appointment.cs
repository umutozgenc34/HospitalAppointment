namespace HospitalAppointment.WebApi.Models
{
    public sealed class Appointment : Entity<Guid>
    {
        
        public string PatientName { get; set; }  
        public DateTime AppointmentDate { get; set; }  
        public int DoctorId { get; set; }  //FK
        public Doctor Doctor { get; set; }  
    }
}
