namespace HospitalAppointment.WebApi.Models.Dtos.Appointments.Request;

public class AddAppointmentRequestDto
{
    public string PatientName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DoctorId { get; set; }

    public static explicit operator Appointment(AddAppointmentRequestDto dto)
    {
        return new Appointment
        {
            PatientName = dto.PatientName, 
            AppointmentDate = dto.AppointmentDate,
            DoctorId = dto.DoctorId
        };
    }
}
