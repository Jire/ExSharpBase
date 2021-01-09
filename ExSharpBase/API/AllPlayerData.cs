using System;
using System.Collections.Generic;
using System.Linq;
using ExSharpBase.API.Models;

namespace ExSharpBase.API
{
    public static class AllPlayerData
    {
        public static IEnumerable<PlayerData> AllPlayers => GetAllPlayers();

        public static PlayerData FirstPlayer => GetPlayerData(0);

        public static PlayerData SecondPlayer => GetPlayerData(1);

        public static PlayerData ThirdPlayer => GetPlayerData(2);

        public static PlayerData FourthPlayer => GetPlayerData(3);

        public static PlayerData FifthPlayer => GetPlayerData(4);

        public static PlayerData SixthPlayer => GetPlayerData(5);

        public static PlayerData SeventhPlayer => GetPlayerData(6);

        public static PlayerData EighthPlayer => GetPlayerData(7);

        public static PlayerData NinthPlayer => GetPlayerData(8);

        public static PlayerData TenthPlayer => GetPlayerData(9);

        public static PlayerData EleventhPlayer => GetPlayerData(10);

        public static PlayerData TwelfthPlayer => GetPlayerData(11);

        private static PlayerData GetPlayerData(int playerId)
        {
            var allPlayerData = Service.GetAllPlayerData();
            if (allPlayerData.Count <= playerId)
            {
                throw new IndexOutOfRangeException($"player: {playerId} is not available");
            }

            var savedPlayerData = allPlayerData[playerId];
            var newPlayerData = new PlayerData
            {
                ChampionName = savedPlayerData["championName"]?.ToString(),
                IsBot = savedPlayerData["isBot"].ToObject<bool>(),
                IsDead = savedPlayerData["isDead"].ToObject<bool>(),
                Level = savedPlayerData["level"].ToObject<int>(),
                RolePosition = savedPlayerData["position"]?.ToString(),
                RawChampionName = savedPlayerData["rawChampionName"]?.ToString(),
                RespawnTimer = savedPlayerData["respawnTimer"].ToObject<float>(),
                SkinID = savedPlayerData["skinID"].ToObject<int>(),
                SummonerName = savedPlayerData["summonerName"]?.ToString(),
                Team = savedPlayerData["team"]?.ToString()
            };
            return newPlayerData;
        }

        private static IEnumerable<PlayerData> GetAllPlayers()
        {
            return Service.GetAllPlayerData()
                .Select(playerData => new PlayerData
                {
                    ChampionName = playerData["championName"].ToString(),
                    IsBot = playerData["isBot"].ToObject<bool>(),
                    IsDead = playerData["isDead"].ToObject<bool>(),
                    Level = playerData["level"].ToObject<int>(),
                    RolePosition = playerData["position"].ToString(),
                    RawChampionName = playerData["rawChampionName"].ToString(),
                    RespawnTimer = playerData["respawnTimer"].ToObject<float>(),
                    SkinID = playerData["skinID"].ToObject<int>(),
                    SummonerName = playerData["summonerName"].ToString(),
                    Team = playerData["team"].ToString()
                })
                .ToList();
        }
    }
}