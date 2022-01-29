using Controls;
using Photon.Pun.UtilityScripts;
using UnityEngine.Serialization;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        public JoystickControlInfo JoystickControlInfo;
        public PhotonTeamsManager TeamsManager;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(JoystickControlInfo)
                .AsSingle();

            Container
                .BindInstance(TeamsManager)
                .AsSingle();
        }
    }
}