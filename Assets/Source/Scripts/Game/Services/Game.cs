using GGJ2022.Source.Scripts.Game.StateMachine;
using GGJ2022.Source.Scripts.Game.StateMachine.Base;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Services
{
    public class Game : IInitializable
    {
        private GameStateMachine _gameStateMachine;

        public Game(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Initialize()
        {  
            PhotonNetwork.AutomaticallySyncScene = true;
            Application.targetFrameRate = 60;
            _gameStateMachine.Fire(GameTriggers.StartConnect);
        }
    }
}