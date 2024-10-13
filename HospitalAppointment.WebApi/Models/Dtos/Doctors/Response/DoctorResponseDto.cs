namespace HospitalAppointment.WebApi.Models.Dtos.Doctors.Response
{
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }

        //implicit donusum
        public static implicit operator DoctorResponseDto(Doctor doctor)
        {
            return new DoctorResponseDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Branch = doctor.Branch.ToString() // enum to string
            };
        }
    }
}
