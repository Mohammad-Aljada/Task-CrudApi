using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_CrudApi.Data.Models;
using Task_CrudApi.Data;
using Task_CrudApi.Dto_s.Department;
using Microsoft.EntityFrameworkCore;

namespace Task_CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
              dtos => new GetAllDepartmentDto()
             {
                  Id = dtos.Id,
                  Name = dtos.Name,
             }
            );

            return Ok(departments);
        }

        [HttpGet("Details")]
        public IActionResult GetbyId(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department Not Found");
            }
            var departmentDto = new DetailsDepartmentDto()
            {
                Name = department.Name
            };


            return Ok(departmentDto);
        }

        [HttpPost("Create")]

        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
          Department department = new Department()
            {
                Name = departmentDto.Name,
            };
            context.Departments.Add(department);
            context.SaveChanges();
            return Ok(department);
        }

        [HttpPut("Update")]

        public IActionResult Update(int id, UpdateDepartmentDto dto)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department Not Found");
            }
            department.Name = dto.Name;

            context.SaveChanges();

            var updatedDepartmentDto = new DetailsDepartmentDto()
            {
                Name = department.Name
            };



            return Ok(updatedDepartmentDto);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department Not Found");
            }
            DeleteDepartmentDto departmentDto = new DeleteDepartmentDto() {
                Id=department.Id ,
                Name = department.Name 
            };
            context.Departments.Remove(department);
            context.SaveChanges();
            return Ok(departmentDto);
        }
    }
}
