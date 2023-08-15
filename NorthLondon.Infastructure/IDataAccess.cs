using NorthLondon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthLondon.Infastructure
{
    public interface IDataAccess
    {
        List<PlayerInfo> GetTotalSquad();
        PlayerInfo GetPlayerByShirtNumber(string shirtId);
        List<PlayerInfo> GetPlayerByNationality(string nationality);
        void DeletePlayerByShirtNumber(string ShirtNumber);
        void UpdatePlayer(PlayerInfo playerInfo);
        void AddPlayer (PlayerInfo playerInfo);
    }
}
