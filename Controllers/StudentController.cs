using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs.StudentDTOs;
using RestFullWebApi.Implementation.Services;

namespace RestFullWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService studentService;

		public StudentController(IStudentService studentService)
		{
			this.studentService = studentService;
		}


		[HttpGet("[action]")]
		public async Task<IActionResult> GetAllStudent()
		{
			var result = await studentService.GetAllStudent();
			return StatusCode(result.StatusCode, result);
		}
		
		
		[HttpGet("[action]/{Id}")]
		public async Task<IActionResult> GetStudentById(int id)
		{
			var result = await studentService.GetStudentById(id);
			return StatusCode(result.StatusCode, result);
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> AddStudent(StudentCreateDTO studentCreateDTO)
		{
			var result = await studentService.AddStudent(studentCreateDTO);
			return StatusCode(result.StatusCode, result);
		}


		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateStudent(StudentUpdateDTO studentUpdateDTO,int id)
		{
			var result = await studentService.UpdateStudent(studentUpdateDTO,id);
			return StatusCode(result.StatusCode, result);
		}


		[HttpDelete("[action]")]
		public async Task<IActionResult> DeleteStudent(int id)
		{
			var result = await studentService.DeleteStudent(id);
			return StatusCode(result.StatusCode, result);
		}


		[HttpPut("[action]/{studentId}/{newStudentId}")]
		public async Task<IActionResult> CahngeStudent(int studentId, int newStudentId)
		{
			var data = await studentService.ChangeSchool(studentId, newStudentId);
			return StatusCode(data.StatusCode, data);
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> ChangeStudent([FromBody] ChangeSchoolDTO model)
		{
			var data = await studentService.ChangeSchool(model);
			return StatusCode(data.StatusCode, data);
		}
	}
}
