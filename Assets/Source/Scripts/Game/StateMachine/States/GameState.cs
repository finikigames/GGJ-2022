using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Services.CameraResolve.Base;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class GameState : IEnter, IExit
    {
        private readonly GameConfig _gameConfig;
        private readonly GameScope _gameScope;
        private readonly ICameraResolveService _cameraResolveService;

        public GameState(GameConfig gameConfig,
                         GameScope gameScope,
                         ICameraResolveService cameraResolveService)
        {
            _gameConfig = gameConfig;
            _gameScope = gameScope;
            _cameraResolveService = cameraResolveService;
        }
        
        public void OnEntry()
        {
            _gameScope.LocalPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, Vector3.zero, Quaternion.identity);
            _cameraResolveService.Resolve();
        }

        public void OnExit()
        {
        }

        public void ShowResults()
        {
            
        }

        public void HideResults()
        {
            
        }
    }
}