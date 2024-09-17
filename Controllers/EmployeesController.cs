using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using Webapi.Repositories.Interfaces;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = _mapper.Map<Employee>(employeeDto);
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeCreateDto employeeDto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            _mapper.Map(employeeDto, employee);
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        // SEARCH: api/employees/search?name=John
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name = null, [FromQuery] string phoneNumber = null, [FromQuery] int? deptId = null)
        {
            var employees = await _unitOfWork.Employees.FindAsync(e =>
                (string.IsNullOrEmpty(name) || e.Name.Contains(name)) &&
                (string.IsNullOrEmpty(phoneNumber) || e.PhoneNumber.Contains(phoneNumber)) &&
                (!deptId.HasValue || e.Deptid == deptId));

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }
    }

}
