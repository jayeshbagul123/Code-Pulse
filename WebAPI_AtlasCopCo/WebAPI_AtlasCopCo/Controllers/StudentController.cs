using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_AtlasCopCo.Model;
using WebAPI_AtlasCopCo.Repository.Interface;

namespace WebAPI_AtlasCopCo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudent _studentRepository;
        public StudentController(ILogger<StudentController> logger, IStudent studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]           
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Students student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }
            var addedStudent = await _studentRepository.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = addedStudent.StdId }, addedStudent);
        }
    }
}
