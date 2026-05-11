using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{

    //[ApiController]
    //[Route("api/[controller]")]
    //public class PatientsController : ControllerBase
    //{
    //    private readonly MedicalContext _context;

    //    public PatientsController(MedicalContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/patients
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    //    {
    //        return await _context.Patients.ToListAsync();
    //    }

    //    // POST: api/patients
    //    [HttpPost]
    //    public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
    //    {
    //        _context.Patients.Add(patient);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(GetPatients), new { id = patient.Id }, patient);
    //    }
    //}
}