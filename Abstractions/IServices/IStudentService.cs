using RestFullWebApi.DTOs.StudentDTOs;
using RestFullWebApi.Models;

namespace RestFullWebApi.Abstractions.IServices
{
    public interface IStudentService
    {
        Task<GenericResponseModel<List<StudentGetDTO>>> GetAllStudent();

        Task<GenericResponseModel<StudentCreateDTO>> AddStudent(StudentCreateDTO student);

        Task<GenericResponseModel<bool>> UpdateStudent(StudentUpdateDTO student,int Id);

        Task<GenericResponseModel<bool>> DeleteStudent(int Id);

        Task<GenericResponseModel<StudentGetDTO>> GetStudentById(int Id);

        Task<GenericResponseModel<StudentGetDTO>> GetAllStudentId(int Id);


        Task<GenericResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId);

        Task<GenericResponseModel<bool>> ChangeSchool(ChangeSchoolDTO change);

    }
}
