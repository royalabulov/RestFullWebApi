using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFullWebApi.Abstractions.Services;
using RestFullWebApi.Context;
using RestFullWebApi.DTOs.SchoolDTOs;
using RestFullWebApi.Entity.ApplicationEntites;
using RestFullWebApi.Models;

namespace RestFullWebApi.Implementation.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;

        public SchoolService(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }
        public async Task<GenericResponseModel<List<SchoolGetDTO>>> GetAllSchoolsAsync()
        {
            var responseModel = new GenericResponseModel<List<SchoolGetDTO>>()
            {
                Data = null,
                StatusCode = 400
            };
            try
            {
                var responseEntity = await _appDBContext.Schools.ToListAsync();
                if (responseEntity != null && responseEntity.Count > 0)
                {
                    var mapResponse = _mapper.Map<List<SchoolGetDTO>>(responseEntity);
                    responseModel.Data = mapResponse;
                    responseModel.StatusCode = 200;

                }
                return responseModel;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                responseModel.StatusCode = 500;
                return responseModel;
            }
        }

        public async Task<GenericResponseModel<SchoolCreateDTO>> AddSchoolAsync(SchoolCreateDTO schoolCreateDTO)
        {
            var responseModel = new GenericResponseModel<SchoolCreateDTO>()
            {
                Data = schoolCreateDTO,
                StatusCode = 400

            };
            try
            {
                if (schoolCreateDTO is not null)
                {

                    School schoolEntity = _mapper.Map<School>(schoolCreateDTO);

                    await _appDBContext.AddAsync(schoolEntity);
                }
                int result = await _appDBContext.SaveChangesAsync();

                if (result > 0)
                {
                    responseModel.StatusCode = 201;
                }
                else
                {
                    responseModel.StatusCode = 500;
                }

            }
            catch
            {
                responseModel.StatusCode = 500;
            }
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> DeleteSchoolAsync(int id)
        {
            var responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400
            };
            try
            {
                var responseEntity = await _appDBContext.Schools.FindAsync(id);

                if (responseEntity != null)
                {
                    _appDBContext.Schools.Remove(responseEntity);
                    var result = await _appDBContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Data = true;
                    }
                }

            }
            catch (Exception ex)
            {
                responseModel.StatusCode = 500;
            }
            return responseModel;

        }


        public async Task<GenericResponseModel<SchoolGetDTO>> GetSchoolByIDAsync(int id)
        {
            var responseModel = new GenericResponseModel<SchoolGetDTO>()
            {
                Data = null,
                StatusCode = 400

            };
            var response = await _appDBContext.Schools.FindAsync(id);

            if (response != null)
            {
                SchoolGetDTO mapResponse = _mapper.Map<SchoolGetDTO>(response);
                responseModel.Data = mapResponse;
                responseModel.StatusCode = 200;
            }
            return responseModel;
        }

        public async Task<GenericResponseModel<bool>> UpdateSchoolAsync(SchoolUpdateDTO schoolUpdateDTO)
        {
            var responseModel = new GenericResponseModel<bool>()
            {
                Data = false,
                StatusCode = 400

            };
            if (schoolUpdateDTO != null)
            {
                var responseEntity = await _appDBContext.Schools.FindAsync(schoolUpdateDTO.ID);
                if (responseEntity != null)
                {
                    responseEntity.Name = schoolUpdateDTO.Name;
                    responseEntity.Number = schoolUpdateDTO.Number;

                    _appDBContext.Schools.Update(responseEntity);
                    var result = await _appDBContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        responseModel.Data = true;
                        responseModel.StatusCode = 200;
                    }
                    else
                    {
                        responseModel.StatusCode = 500;
                    }
                }
            }
            return responseModel;
        }
    }
}


