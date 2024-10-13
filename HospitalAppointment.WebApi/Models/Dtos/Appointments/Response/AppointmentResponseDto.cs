namespace HospitalAppointment.WebApi.Models.Dtos.Appointments.Response;

public class AppointmentResponseDto
{
    public string PatientName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DoctorId { get; set; }

    //implicit donusum
    public static implicit operator AppointmentResponseDto(Appointment dto)
    {
        return new AppointmentResponseDto
        {
            PatientName = dto.PatientName,
            AppointmentDate = dto.AppointmentDate,
            DoctorId = dto.DoctorId
        };
    }
}
