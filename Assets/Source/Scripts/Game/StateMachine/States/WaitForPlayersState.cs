using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.StateMachine.States.Base;
using Photon.Realtime;

namespace GGJ2022.Source.Scripts.Game.StateMachine.States
{
    public class WaitForPlayersState : IConnectionCallbacks, IEnter, IExit
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
            
        }

        public void OnExit()
        {
        }
    }
}