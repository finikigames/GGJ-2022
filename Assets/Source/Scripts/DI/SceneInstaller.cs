using GGJ2022.Source.Scripts.Controls;
using GGJ2022.Source.Scripts.Game.ECS;
using GGJ2022.Source.Scripts.Game;
using GGJ2022.Source.Scripts.Game.StateMachine;
using GGJ2022.Source.Scripts.Game.StateMachine.States;
using Photon.Pun.UtilityScripts;
using Source.Scripts.Core.Ticks;
using Zenject;

namespace GGJ2022.Source.Scripts.DI
{
    public class SceneInstaller : MonoInstaller
    {
        public JoystickControlInfo JoystickControlInfo;
        public PhotonTeamsManager TeamsManager;
        public InitState InitState;
        public UpdateService UpdateService;

        public override void InstallBindings()
        {
            Container
                .Bind<EcsStartup>()
                .AsSingle();

            Container
                .BindInstance(UpdateService)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<Game.Services.Game>()
                .AsSingle();
            
            Container
                .BindInstance(JoystickControlInfo)
                .AsSingle();
            
            Container
                .BindInstance(InitState)
                .AsSingle();

            Container
                .Bind<GameScope>()
                .AsSingle();
            
            Container
                .BindInstance(TeamsManager)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle();
            
            StateInstaller.Install(Container);
        }
    }
}