using System.Collections.Generic;
using DG.Tweening;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Services
{
    public class WinService : MonoBehaviourPunCallbacks
    {
        public PhotonView PhotonView;
        public CanvasGroup Group;

        private GameConfig _gameConfig;
        private PhotonTeamsManager _photonTeamsManager;

        [Inject]
        public void Construct(GameConfig gameConfig,
                              PhotonTeamsManager photonTeamsManager)
        {
            _gameConfig = gameConfig;
            _photonTeamsManager = photonTeamsManager;
        }
        
        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this); 
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this); 
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (_gameConfig.isDeathmatch && PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                ShowWinScreen();
            }

            if (!_gameConfig.isDeathmatch && PhotonNetwork.CurrentRoom.PlayerCount <= 2)
            {
                var uniqueTeam = new HashSet<byte>(3);
                var players = PhotonNetwork.CurrentRoom.Players;

                foreach (var playerPair in players)
                {
                    var team = playerPair.Value.GetPhotonTeam();
                    uniqueTeam.Add(team.Code);
                }

                if (uniqueTeam.Count == 1)
                {
                    ShowWinScreen();
                }
            }
        }

        private void ShowWinScreen()
        {
            PhotonView.RPC("RPC_ShowWinScreen", RpcTarget.All);
        }

        [PunRPC]
        private void RPC_ShowWinScreen()
        {
            Group.DOFade(_gameConfig.WinScreenShowTime, 1f);
        }
    }
}