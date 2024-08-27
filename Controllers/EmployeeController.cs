using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prueba_Back.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Prueba_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly AplicationDBcontext _context;
        public EmployeeController(AplicationDBcontext context)
        {
            _context = context;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _context.Employees.FromSqlRaw("sp_listaEmpleados").ToListAsync();
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> obtenerEmpleado(int id)
        {
            try
            {
                var employee = _context.Employees.Find(id);

                if (employee == null)
                {
                    return StatusCode(404);
                }
                return Ok(employee);
            }
            catch
            {
                return StatusCode(500);
            }
        }



        // POST api/<EmployeeController>
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee newEmployee)
        {
            try
            {
                _context.Employees.Add(newEmployee);
                _context.SaveChangesAsync();

                return StatusCode(201, newEmployee);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Employee updateEmployee)
        {
            try
            {
                if (updateEmployee == null)
                {
                    return StatusCode(404);
                }
                if (id != updateEmployee.EmployeeId)
                {
                    return BadRequest("Employee ID mismatch.");
                }
                _context.Update(updateEmployee);
                await _context.SaveChangesAsync();

                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //Esto es para obtener los empleados que fueron contratados después de una fecha específica.
        [HttpGet("HireDate")]
        public async Task<IActionResult> ObtenerFechaDespuesContratoEmpleados([FromQuery] DateTime hireDate)
        {
            try
            {
                var employees = await _context.Employees
                    .FromSqlRaw("sp_FechaContratoEmpleados @HireDate", new SqlParameter("@HireDate", hireDate))
                    .ToListAsync();

                return StatusCode(200, employees);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
