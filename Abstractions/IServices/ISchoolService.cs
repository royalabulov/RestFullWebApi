using RestFullWebApi.DTOs.SchoolDTOs;
using RestFullWebApi.Models;

namespace RestFullWebApi.Abstractions.Services
{
    public interface ISchoolService
    {
        Task<GenericResponseModel<List<SchoolGetDTO>>> GetAllSchoolsAsync();

        Task<GenericResponseModel<SchoolCreateDTO>> AddSchoolAsync(SchoolCreateDTO schoolCreateDTO);

        Task<GenericResponseModel<bool>> UpdateSchoolAsync(SchoolUpdateDTO schoolUpdateDTO);

        Task<GenericResponseModel<bool>> DeleteSchoolAsync(int id);

        Task<GenericResponseModel<SchoolGetDTO>> GetSchoolByIDAsync(int id);


    }
}
