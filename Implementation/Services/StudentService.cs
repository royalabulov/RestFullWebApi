using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestFullWebApi.Abstractions;
using RestFullWebApi.Abstractions.IRepositories.ISchoolRepositories;
using RestFullWebApi.Abstractions.IRepositories.IStudentRepositories;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.Context;
using RestFullWebApi.DTOs.StudentDTOs;
using RestFullWebApi.Entity.ApplicationEntites;
using RestFullWebApi.Implementation.UnitOfWork;
using RestFullWebApi.Models;

namespace RestFullWebApi.Implementation.Services
{
	public class StudentService : IStudentService
	{
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;


		public StudentService(IMapper mapper, IUnitOfWork unitOfWork)
		{

			this.mapper = mapper;
			this.unitOfWork = unitOfWork;

		}


		public async Task<GenericResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId)
		{
			var response = new GenericResponseModel<bool>();
			try
			{
				var student = await unitOfWork.GetRepository<Student>().GetByIdAsync(studentId);
				var newSchool = await unitOfWork.GetRepository<School>().GetByIdAsync(newSchoolId);

				if (student == null || newSchool == null)
				{
					response.Data = false;
					response.StatusCode = 404;
					return response;
				}
				student.School = newSchool;

				unitOfWork.GetRepository<Student>().Update(student);
				var result = await unitOfWork.Commit();

				if (result > 0)
				{
					response.Data = true;
					response.StatusCode = 200;
				}
				else
				{
					response.Data = false;
					response.StatusCode = 500;
				}
			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				Console.WriteLine(ex.Message);

			}

			return response;
		}

		public async Task<GenericResponseModel<bool>> ChangeSchool(ChangeSchoolDTO change)
		{
			var response = new GenericResponseModel<bool>();
			try
			{
				var student = await unitOfWork.GetRepository<Student>().GetByIdAsync(change.StudentId);
				var newSchool = await unitOfWork.GetRepository<School>().GetByIdAsync(change.NewSchoolId);

				if (student == null || newSchool == null)
				{
					response.Data = false;
					response.StatusCode = 404;
					return response;
				}

				student.School = newSchool;

				unitOfWork.GetRepository<Student>().Update(student);
				var result = await unitOfWork.Commit();

				if (result > 0)
				{
					response.Data = true;
					response.StatusCode = 200;
				}
				else
				{
					response.Data = false;
					response.StatusCode = 500;
				}
			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				Console.WriteLine(ex.Message);

			}

			return response;
		}





		public async Task<GenericResponseModel<List<StudentGetDTO>>> GetAllStudent()
		{
			var response = new GenericResponseModel<List<StudentGetDTO>>()
			{
				Data = null,
				StatusCode = 400
			};

			try
			{
				var responseEntity = await unitOfWork.GetRepository<Student>().GetAll().Include(x => x.School).ToListAsync();
				if (responseEntity != null)
				{
					var mapEntity = mapper.Map<List<StudentGetDTO>>(responseEntity);
					response.Data = mapEntity;
					response.StatusCode = 200;
				}
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				response.StatusCode = 500;
				return response;
			}
		}

		public Task<GenericResponseModel<StudentGetDTO>> GetAllStudentId(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<GenericResponseModel<StudentGetDTO>> GetStudentById(int Id)
		{
			var response = new GenericResponseModel<StudentGetDTO>()
			{
				Data = null,
				StatusCode = 400
			};

			try
			{
				var responseEntity = await unitOfWork.GetRepository<Student>().GetByIdAsync(Id);
				if (responseEntity != null)
				{
					var schoolResponse = await unitOfWork.GetRepository<School>().GetByIdAsync(responseEntity.SchoolId);
					var mapEntity = mapper.Map<StudentGetDTO>(responseEntity);

					response.Data = mapEntity;
					response.StatusCode = 200;
				}
				return response;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				response.StatusCode = 500;
				return response;
			}
		}

		public async Task<GenericResponseModel<StudentCreateDTO>> AddStudent(StudentCreateDTO student)
		{
			var response = new GenericResponseModel<StudentCreateDTO>()
			{
				Data = null,
				StatusCode = 400
			};

			try
			{
				if (student == null)
				{
					return response;
				}
				var mapRepo = mapper.Map<Student>(student);
				await unitOfWork.GetRepository<Student>().AddAsync(mapRepo);



				var result = await unitOfWork.Commit();
				if (result > 0)
				{
					response.StatusCode = 201;
					response.Data = student;
				}
				return response;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				response.StatusCode = 500;
				return response;
			}
		}


		public async Task<GenericResponseModel<bool>> DeleteStudent(int Id)
		{
			var response = new GenericResponseModel<bool>()
			{
				Data = false,
				StatusCode = 400
			};

			try
			{
				var Student = await unitOfWork.GetRepository<Student>().GetByIdAsync(Id);

				var removeStudent = unitOfWork.GetRepository<Student>().Remove(Student);

				var result = await unitOfWork.Commit();
				if (result > 0)
				{
					response.StatusCode = 201;
					response.Data = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				response.StatusCode = 500;
			}
			return response;
		}


		public async Task<GenericResponseModel<bool>> UpdateStudent(StudentUpdateDTO student, int Id)
		{
			var response = new GenericResponseModel<bool>()
			{
				Data = false,
				StatusCode = 400
			};

			try
			{

				var Student = await unitOfWork.GetRepository<Student>().GetByIdAsync(Id);

				var map = mapper.Map(student, Student);
				var entityRepo = unitOfWork.GetRepository<Student>().Update(map);

				var result = await unitOfWork.Commit();
				if (result > 0)
				{
					response.StatusCode = 201;
					response.Data = true;
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				response.StatusCode = 500;
			}
			return response;

		}

	}
}
