using ExitGames.Client.Photon;
using GGJ2022.Source.Scripts.Game.StateMachine;
using GGJ2022.Source.Scripts.Game.StateMachine.Base;
using GGJ2022.Source.Scripts.Global;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Services
{
    public class Game : IInitializable, IOnEventCallback
    {
        private readonly GameStateMachine _gameStateMachine;

        public Game(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {  
            PhotonNetwork.AutomaticallySyncScene = true;
            Application.targetFrameRate = 60;
            
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            if (eventCode == PhotonEvents.StartGame)
            {
                _gameStateMachine.Fire(GameTriggers.StartGame);
            }

            if (eventCode == PhotonEvents.StartWaiting)
            {
                _gameStateMachine.Fire(GameTriggers.StartConnect);
            }
        }
    }
}