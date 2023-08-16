using Microsoft.Extensions.Options;
using NorthLondon.Domain;
using NorthLondon.Infastructure.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthLondon.Infastructure
{
    public class DataAccess : IDataAccess
    {
        private readonly ConnectionStrings _options;

        public DataAccess(IOptions<ConnectionStrings> options)
        {
            _options = options.Value;
        }
        public void AddPlayer(PlayerInfo playerInfo)
        {
            var query = @"INSERT INTO PLAYERS (Name, ShirtNumber, Position, Nationality, JoinDate)
                  VALUES (@Name, @ShirtNumber, @Position, @Nationality, @JoinDate)";

            using SqlConnection sqlConnection = new SqlConnection(_options.DefaultConnections);
            sqlConnection.Open();
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@Name", playerInfo.Name);
            cmd.Parameters.AddWithValue("@ShirtNumber", playerInfo.ShirtNumber);
            cmd.Parameters.AddWithValue("@Position", playerInfo.Position);
            cmd.Parameters.AddWithValue("@Nationality", playerInfo.Nationality);
            cmd.Parameters.AddWithValue("@JoinDate", playerInfo.JoinDate);
            cmd.ExecuteNonQuery();              
        }

        public void DeletePlayerByShirtNumber(string ShirtNumber)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            var query = @"DELETE FROM PLAYERS WHERE ShirtNumber = @shirtNumber";
            using SqlConnection sqlConnection = new SqlConnection(_options.DefaultConnections);
            sqlConnection.Open();
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@shirtNumber", ShirtNumber);
            cmd.ExecuteNonQuery();
        }

        public List<PlayerInfo> GetPlayerByNationality(string nationality)
        {
            List<PlayerInfo> playerInfos = new List<PlayerInfo>();
            var query = @"SELECT * FROM PLAYERS WHERE Nationality = @nationality";
            using SqlConnection sqlConnection = new SqlConnection(_options.DefaultConnections);
            sqlConnection.Open();
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@nationality", nationality);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PlayerInfo playerInfo = new PlayerInfo();
                playerInfo.PlayerID = (int)reader["Id"];
                playerInfo.Name = reader["Name"].ToString();
                playerInfo.ShirtNumber = reader["ShirtNumber"].ToString();
                playerInfo.Position = reader["Position"].ToString();
                playerInfo.Nationality = reader["Nationality"].ToString();
                playerInfo.JoinDate = Convert.ToDateTime(reader["DateJoined"]);

                playerInfos.Add(playerInfo);
            }
            reader.Close();
            return playerInfos;
        }

        public PlayerInfo GetPlayerByShirtNumber(string shirtId)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            var query = @"SELECT * FROM PLAYERS WHERE ShirtNumber = @shirtId";
            using SqlConnection sqlConnection = new SqlConnection(_options.DefaultConnections);
            sqlConnection.Open();
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@shirtId", shirtId);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                playerInfo.PlayerID = (int)reader["Id"];
                playerInfo.Name = reader["Name"].ToString();
                playerInfo.ShirtNumber = reader["ShirtNumber"].ToString();
                playerInfo.Position = reader["Position"].ToString();
                playerInfo.Nationality = reader["Nationality"].ToString();
                playerInfo.JoinDate = Convert.ToDateTime(reader["DateJoined"]);
                playerInfo.PreferredFoot = reader["PreferredFoot"].ToString();
            }
            reader.Close();
            return playerInfo;
        }



        public List<PlayerInfo> GetTotalSquad()
        {
            List<PlayerInfo> playerInfo = new List<PlayerInfo>();
            using SqlConnection connection = new SqlConnection(_options.DefaultConnections);
            connection.Open();
            var query = @"SELECT * FROM PLAYERS";
            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PlayerInfo player = new PlayerInfo
                {
                    PlayerID = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    ShirtNumber = reader["ShirtNumber"].ToString(),
                    JoinDate = Convert.ToDateTime(reader["DateJoined"]),
                    PreferredFoot = reader["PreferredFoot"].ToString(),
                    Position = reader["Position"].ToString(),
                    Nationality = reader["Nationality"].ToString()
            };
                playerInfo.Add(player);
            }
            reader.Close();
            return playerInfo;
        }


        public void UpdatePlayer(PlayerInfo playerInfo)
        {
            var query = @"UPDATE PLAYERS SET Name = @Name,ShirtNumber = @ShirtNumber,Position = @Position,
                          Nationality = @Nationality,JoinDate = @JoinDate WHERE PlayerID = @PlayerID";
            using SqlConnection sqlConnection = new SqlConnection(_options.DefaultConnections);
            sqlConnection.Open();
            using SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@Name", playerInfo.Name);
            cmd.Parameters.AddWithValue("@ShirtNumber", playerInfo.ShirtNumber);
            cmd.Parameters.AddWithValue("@Position", playerInfo.Position);
            cmd.Parameters.AddWithValue("@Nationality", playerInfo.Nationality);
            cmd.Parameters.AddWithValue("@JoinDate", playerInfo.JoinDate);
            cmd.Parameters.AddWithValue("@PlayerID", playerInfo.PlayerID);
            cmd.ExecuteNonQuery();
        }
    }
}

                   
