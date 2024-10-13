using HospitalAppointment.WebApi.Models.Enums;

namespace HospitalAppointment.WebApi.Models.Dtos.Doctors.Request;

public class AddDoctorRequestDto
{
    public string Name { get; set; }
    public BranchType Branch { get; set; }

    //explicit donusum
    public static explicit operator Doctor(AddDoctorRequestDto requestDto)
    {
        return new Doctor
        {
            Name = requestDto.Name,
            Branch = requestDto.Branch
        };
    }
}
