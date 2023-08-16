using Microsoft.Extensions.Logging;
using NorthLondon.Application.ApplicationException;
using NorthLondon.Domain;
using NorthLondon.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthLondon.Application
{
     public class PlayerService : IPlayerService
    {
        private readonly IDataAccess _context;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IDataAccess context,ILogger<PlayerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddANewPlayer(PlayerInfo playerInfo)
        {
            var player = _context.GetPlayerByShirtNumber(playerInfo.ShirtNumber);
            if (player == null) 
            {
                _context.AddPlayer(playerInfo);
                _logger.LogInformation($"New player with shirt Number {playerInfo.ShirtNumber} added successfully.");
            }
            else
            {
                _logger.LogWarning("Player with the given shirt number already exists.");
                throw new PlayerAlreadyExistException(playerInfo.ShirtNumber);
            }
        }

        public void DeleteDepartedPlayerByShirtNumber(string ShirtNumber)
        {
            var player = _context.GetPlayerByShirtNumber(ShirtNumber); 
            if (player == null) 
            {
                _logger.LogWarning("Player with the given shirt number doesnt exists.");
                throw new PlayerDoesntExistException(ShirtNumber);
            }
            else
            {
                _context.DeletePlayerByShirtNumber(ShirtNumber) ;
                _logger.LogInformation($"Player with ShirtNumber {ShirtNumber} deleted successfully");
            }
        }

        public List<PlayerInfo> GetAllPlayersByNationality(string nationality)
        {
            var players = _context.GetPlayerByNationality(nationality);
            return players;
        }

        public PlayerInfo GetPlayerByShirtNumber(string shirtId)
        {
            var player = _context.GetPlayerByShirtNumber(shirtId); 
            if(player.ShirtNumber == null) 
            {
                _logger.LogWarning("Player with the given shirt number doesnt exists.");
                throw new PlayerDoesntExistException(shirtId);
            }
            return player;
        }

        public List<PlayerInfo> GetTotalSquad()
        {
            var players = _context.GetTotalSquad();
            return players;
        }

        public void UpdatePlayerRecords(PlayerInfo playerInfo)
        {
            var player = _context.GetPlayerByShirtNumber(playerInfo.ShirtNumber);
            if (player == null )
            {
                _logger.LogWarning("Player with the given shirt number cannot be updated because it doesnt exists.");
                throw new PlayerDoesntExistException(playerInfo.ShirtNumber);
            }
            else
            {
                _context.UpdatePlayer(playerInfo) ;
                _logger.LogInformation($"Player with ShirtNumber {playerInfo.ShirtNumber} Updated successfully");
            }
        }
    }
}
