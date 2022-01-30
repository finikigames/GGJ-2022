using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace GGJ2022.Source.Scripts.Global
{
    public static class PhotonEvents
    {
        public const byte StartGame = 1;
        public const byte StartWaiting = 2;
        
        public static void RaiseEvent(byte eventType)
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
            PhotonNetwork.RaiseEvent(eventType, null, raiseEventOptions, SendOptions.SendReliable);
        }
    }
}