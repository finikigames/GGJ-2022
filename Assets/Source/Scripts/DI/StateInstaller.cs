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
            var initState = new InitState();
            var waitForPlayerState = new WaitForPlayersState();
            var gameState = new GameState();
            
            PhotonNetwork.AddCallbackTarget(initState);
            PhotonNetwork.AddCallbackTarget(waitForPlayerState);
            PhotonNetwork.AddCallbackTarget(gameState);

            Container
                .BindInstance(initState)
                .AsSingle();

            Container
                .BindInstance(waitForPlayerState)
                .AsSingle();

            Container
                .BindInstance(gameState)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle();
        }
    }
}