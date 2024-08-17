  using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullWebApi.Abstractions.Services;
using RestFullWebApi.DTOs.SchoolDTOs;

namespace RestFullWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        [HttpGet]
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> GetAll()
        {
            var response = await schoolService.GetAllSchoolsAsync();
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("{id}")]
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> GetById(int id)
        {
            var response = await schoolService.GetSchoolByIDAsync(id);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(SchoolCreateDTO schoolCreateDTO)
        {
            var response = await schoolService.AddSchoolAsync(schoolCreateDTO);
            return StatusCode(response.StatusCode, response);
                        
        }


        [HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Update(SchoolUpdateDTO schoolUpdateDTO)
        {
            var response = await schoolService.UpdateSchoolAsync(schoolUpdateDTO);
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Remove(int id)
        {
            var response = await schoolService.DeleteSchoolAsync(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}








