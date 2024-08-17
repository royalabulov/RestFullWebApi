using RestFullWebApi.DTOs.RoleDTOs;
using RestFullWebApi.Models;

namespace RestFullWebApi.Abstractions.IServices
{
	public interface IRoleService
	{
		Task<GenericResponseModel<List<RoleGetDTO>>> GetAllRoles();
		Task<GenericResponseModel<RoleGetDTO>> GetRolesById(string Id);
		Task<GenericResponseModel<bool>> CreateRole(string name);
		Task<GenericResponseModel<bool>> UpdateRole(RoleUpdateDTO roleUpdateDTO);
		Task<GenericResponseModel<bool>> DeleteRole(string id);
	}
}
