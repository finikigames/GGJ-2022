using GGJ2022.Source.Scripts.Game.Services;
using GGJ2022.Source.Scripts.Game.StateMachine;
using GGJ2022.Source.Scripts.Game.StateMachine.States;
using Photon.Pun;
using Zenject;

namespace GGJ2022.Source.Scripts.DI
{
    public class StateInstaller : Installer<StateInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<WaitForPlayersState>()
                .AsSingle();
            
            Container
                .Bind<GameState>()
                .AsSingle();

            Container
                .Bind<PlayerService>()
                .AsSingle();
        }
    }
}