using GGJ2022.Source.Scripts.Controls;
using Photon.Pun.UtilityScripts;
using Zenject;

namespace GGJ2022.Source.Scripts.DI
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