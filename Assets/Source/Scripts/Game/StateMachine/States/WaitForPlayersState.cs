using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using GGJ2022.Source.Scripts.Global;
using Photon.Pun;
using Photon.Realtime;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class WaitForPlayersState : IEnter, IExit
    {
        private readonly GameConfig _gameConfig;

        public WaitForPlayersState(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }
        
        private bool CheckForGameReady()
            => PhotonNetwork.CurrentRoom.PlayerCount == _gameConfig.PlayersToStartGame;

        public void OnEntry()
        {
            if (CheckForGameReady())
            {
                PhotonEvents.RaiseEvent(PhotonEvents.StartGame);
            }
        }

        public void OnExit()
        {
        }
    }
}