using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.ECS;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Services.CameraResolve.Base;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Zenject;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class GameState : IEnter, IExit
    {
        private readonly GameConfig _gameConfig;
        private readonly GameScope _gameScope;
        private readonly ICameraResolveService _cameraResolveService;
        private readonly EcsStartup _ecsStartup;
        private readonly PhotonTeamsManager _photonTeamsManager;

        public GameState(GameConfig gameConfig,
                         GameScope gameScope,
                         ICameraResolveService cameraResolveService,
                         EcsStartup ecsStartup,
                         PhotonTeamsManager photonTeamsManager)
        {
            _gameConfig = gameConfig;
            _gameScope = gameScope;
            _cameraResolveService = cameraResolveService;
            _ecsStartup = ecsStartup;
            _photonTeamsManager = photonTeamsManager;
        }
        
        public void OnEntry()
        {
            _ecsStartup.RegisterRunner();
            _gameScope.LocalPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, Vector3.zero, Quaternion.identity);
            _cameraResolveService.Resolve();
            EnterRandomTeam();
        }

        public void OnExit()
        {
            _ecsStartup.UnRegisterRunner();
        }

        public void ShowResults()
        {
            
        }

        public void HideResults()
        {
            
        }

        private void EnterRandomTeam()
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
        }
    }
}