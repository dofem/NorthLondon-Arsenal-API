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


        [HttpGet("GetPlayerByShirtNumber")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse> GetPlayerByShirtNumber(string shirtNumber)
        {
            try
            {
                var result = _playerService.GetPlayerByShirtNumber(shirtNumber);
                _response.Count = result.ShirtNumber.Count();
                _response.StatusCode = HttpStatusCode.OK;
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
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
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

        [HttpPost("RegisterANewPlayer")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse> RegisterANewPlayer(PlayerInfo playerInfo)
        {
            try
            {
                _playerService.AddANewPlayer(playerInfo);
                _response.StatusCode = HttpStatusCode.Created;
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

        [HttpPut("UpdateAPlayerRecord")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse> UpdateAPlayerRecord(PlayerInfo playerInfo)
        {
            try
            {
                _playerService.UpdatePlayerRecords(playerInfo);
                _response.StatusCode = HttpStatusCode.Created;
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


        [HttpPut("DeleteAPlayerRecord")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<ApiResponse> DeleteAPlayerRecord(string shirtNumber)
        {
            try
            {
                _playerService.DeleteDepartedPlayerByShirtNumber(shirtNumber);
                _response.StatusCode = HttpStatusCode.OK;
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
