using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_CrudApi.Data;
using Task_CrudApi.Data.Models;
using Task_CrudApi.Dto_s.Employee;

namespace Task_CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context) 
        {
            this.context = context;
        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var employees = context.Employees.ToList();
            var employeesDto = employees.Adapt<IEnumerable<GetAllEmployeeDto>>();

            return Ok(employeesDto);
        }

        [HttpGet("Details")]
        public IActionResult GetbyId(int id) {
            var employee = context.Employees.Find(id);
            if (employee == null) {
                return NotFound("Employee Not Found");
            }
            var employeeDto = employee.Adapt<DetailsEmployeeDto>();
            return Ok(employeeDto);
        }

        [HttpPost("Create")]

        public IActionResult Create(CreateEmployeeDto dto)
        {
            var employee = dto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok(employee);
        }

        [HttpPut("Update")]

        public IActionResult Update(int id , UpdateEmployeeDto dto) {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee Not Found");
            }

            dto.Adapt(employee);


            context.SaveChanges();
            return Ok(dto);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id) {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee Not Found");
            }
            var dto = employee.Adapt<DetailsEmployeeDto>();
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok(dto);
        }
    }
}
