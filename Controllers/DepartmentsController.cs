using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using Webapi.Repositories.Interfaces;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return Ok(departmentDto);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = _mapper.Map<Department>(departmentDto);
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentCreateDto departmentDto)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            _mapper.Map(departmentDto, department);
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            _unitOfWork.Departments.Remove(department);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        // SEARCH: api/departments/search?name=IT
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var departments = await _unitOfWork.Departments.FindAsync(d => d.Name.Contains(name));
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }
    }

}
