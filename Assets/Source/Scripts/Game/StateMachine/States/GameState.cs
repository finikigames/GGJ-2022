using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.ECS;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using Photon.Pun;
using Photon.Realtime;
using Zenject;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class GameState : IEnter, IExit
    {
        private GameObject _localPlayer;
        
        private readonly GameConfig _gameConfig;
        [Inject]
        private EcsStartup ecs;

        public GameState(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }
        
        public void OnEntry()
        {
            ecs.RegisterRunner();
            _localPlayer = PhotonNetwork.Instantiate(_gameConfig.PlayerPrefab.name, Vector3.zero, Quaternion.identity);
        }

        public void OnExit()
        {
            ecs.UnRegisterRunner();
        }

        public void ShowResults()
        {
            
        }

        public void HideResults()
        {
            
        }
    }
}