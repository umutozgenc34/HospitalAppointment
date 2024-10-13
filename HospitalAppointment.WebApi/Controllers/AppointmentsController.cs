using HospitalAppointment.WebApi.Models;
using HospitalAppointment.WebApi.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointment.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private IAppointmentService _appointmentsService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentsService = appointmentService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _appointmentsService.GetAllAppointments();
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add(Appointment appointment)
    {
        var result = _appointmentsService.AddAppointment(appointment);
        return Ok(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById (Guid id)
    {
        var result = _appointmentsService.GetAppointmentById(id);
        return Ok(result);
    }

    [HttpDelete("delete")]
    public IActionResult Delete(Guid id)
    {
        var result = _appointmentsService.DeleteAppointment(id);
        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult Update(Appointment appointment)
    {
        var result = _appointmentsService.UpdateAppointment(appointment);
        return Ok(result);
    }

    public IActionResult GetAppointmentByDoctorId(int doctorId)
    {
        var result = _appointmentsService.GetAppointmentsByDoctorId(doctorId);
        return Ok(result);
    }
}
