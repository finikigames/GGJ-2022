using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.Services
{
    public class PlayerService
    {
        private readonly GameConfig _gameConfig;
        private readonly GameScope _gameScope;
        private readonly PhotonTeamsManager _photonTeamsManager;
        private readonly TeamSpawnPoints _teamSpawnPoints;
        
        public PlayerService(GameConfig gameConfig,
                                 GameScope gameScope,
                                 PhotonTeamsManager photonTeamsManager,
                                 TeamSpawnPoints teamSpawnPoints)
        {
            _gameConfig = gameConfig;
            _gameScope = gameScope;
            _photonTeamsManager = photonTeamsManager;
            _teamSpawnPoints = teamSpawnPoints;
        }

        public void EnterRandomTeam()
        {
            PhotonTeam[] teams = _photonTeamsManager.GetAvailableTeams();
            var avaliableTeams = new List<byte>();
            foreach (var team in teams)
            {
                _photonTeamsManager.TryGetTeamMembers(team, out var players);
                var playersInTeam = _gameConfig.PlayersToStartGame / 2;
                if (players.Length < (playersInTeam > 0 ? playersInTeam : 1))
                {
                    avaliableTeams.Add(team.Code);
                }
            }
 
            PhotonNetwork.LocalPlayer.JoinTeam(avaliableTeams[Random.Range(0, 2)]);
            if (_gameConfig.isDeathmatch)
            {
                SpawnDeathmatch();
            }
            else
            {
                SpawnPlayer();
            }
        }

        private int FindTeamCode()
        {
            PhotonTeam[] teams = _photonTeamsManager.GetAvailableTeams();
            foreach (var team in teams)
            {
                _photonTeamsManager.TryGetTeamMembers(team, out var players);
                foreach (var player in players)
                {
                    if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                    {
                        return team.Code;
                    }
                }
            }
            return 0;
        }

        private void SpawnPlayer()
        {
            var playerTeam = FindTeamCode();
            var id = PhotonNetwork.LocalPlayer.ActorNumber;
            if (id > 2)
            {
                id /= 2 - 1;
            }
            else
            {
                id /= 2;
            }
            if (playerTeam == 1) 
            {
                _gameScope.LocalPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, _teamSpawnPoints.BlueTeam[id].position, Quaternion.identity);
                return;
            }
            if (playerTeam == 2)
            {
                _gameScope.LocalPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, _teamSpawnPoints.RedTeam[id].position, Quaternion.identity);
                return;
            }
            _gameScope.LocalPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, Vector3.zero, Quaternion.identity);
        }

        private void SpawnDeathmatch()
        {
            var id = PhotonNetwork.LocalPlayer.ActorNumber;
            _gameScope.LocalPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, _teamSpawnPoints.Deathmatch[id-1].position, Quaternion.identity);
        }
    }
}