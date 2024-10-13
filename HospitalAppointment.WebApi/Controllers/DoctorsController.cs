using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Models.Dtos.Doctors.Request;
using HospitalAppointment.WebApi.Models.Enums;
using HospitalAppointment.WebApi.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointment.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _doctorService.GetAll();
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add(AddDoctorRequestDto dto)
    {
        var result = _doctorService.AddDoctor(dto);
        return Ok(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById(int id)
    {
        var result = _doctorService.GetDoctorById(id);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        var result = _doctorService.DeleteDoctor(id);
        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult Update(Doctor doctor)
    {
        var result = _doctorService.UpdateDoctor(doctor);
        return Ok(result);
    }

    [HttpGet("getbybranch")]
    public IActionResult GetByBranch(BranchType branch)
    {
        var result = _doctorService.GetDoctorsByBranch(branch);
        return Ok(result);
    }

}
