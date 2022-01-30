using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using GGJ2022.Source.Scripts.Global;
using Photon.Pun;
using Photon.Realtime;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class InitState : MonoBehaviourPunCallbacks, IEnter
    {
        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this); 
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        public override void OnJoinedRoom()
        {
            PhotonEvents.RaiseEvent(PhotonEvents.StartWaiting);
        }

        public void OnEntry()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}