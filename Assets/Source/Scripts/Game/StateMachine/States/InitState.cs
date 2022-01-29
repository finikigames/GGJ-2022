using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using Photon.Pun;
using Photon.Realtime;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class InitState : IConnectionCallbacks, IEnter
    {
        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnEntry()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}