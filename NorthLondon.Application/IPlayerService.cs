using NorthLondon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthLondon.Application
{
    public interface IPlayerService
    {
        List<PlayerInfo> GetTotalSquad();
        PlayerInfo GetPlayerByShirtNumber(string shirtId);
        List<PlayerInfo> GetAllPlayersByNationality(string nationality);
        void DeleteDepartedPlayerByShirtNumber(string ShirtNumber);
        void UpdatePlayerRecords(PlayerInfo playerInfo);
        void AddANewPlayer(PlayerInfo playerInfo);
    }
}
