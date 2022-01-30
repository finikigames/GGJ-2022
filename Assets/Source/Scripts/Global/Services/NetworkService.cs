using GGJ2022.Source.Scripts.Global.Services.Base;
using Photon.Pun.UtilityScripts;

namespace GGJ2022.Source.Scripts.Global.Services
{
    public class NetworkService : INetworkService
    {
        private PhotonTeamsManager _teamsManager;

        public NetworkService(PhotonTeamsManager teamsManager)
        {
            _teamsManager = teamsManager;
        }
        
        public void ConnectAndJoinTeam()
        {
            
        }
    }
}