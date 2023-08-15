using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthLondon.Application;
using NorthLondon.Domain;
using System.Net;
using X.PagedList;

namespace NorthLondon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ApiResponse _response;

        public PlayerController(IPlayerService playerService) 
        { 
            _playerService = playerService;
            _response = new ApiResponse();
        }

        [HttpGet("GetAllArsenalSquadPlayers")]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse> GetAllArsenalSquadPlayers([FromQuery] int? page = 1, int pageSize = 10)
        {
            try
            {
                var result = _playerService.GetTotalSquad();
                _response.Count = result.Count();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result.ToPagedList(page ?? 1, pageSize);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("GetPlayersByNationality")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse> GetPlayersByNationality(string Nationality,[FromQuery] int? page = 1, int pageSize = 10)
        {
            try
            {
                var result = _playerService.GetAllPlayersByNationality(Nationality);
                _response.Count = result.Count();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result.ToPagedList(page ?? 1, pageSize);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

    }
}
