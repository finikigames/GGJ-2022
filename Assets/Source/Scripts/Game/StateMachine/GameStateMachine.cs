using GGJ2022.Source.Scripts.Game.StateMachine.Base;
using GGJ2022.Source.Scripts.Game.StateMachine.States;
using Stateless;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.StateMachine
{
    public class GameStateMachine : IInitializable
    {
        private StateMachine<GameStates, GameTriggers> _stateMachine;
        private readonly InitState _initState;
        private readonly WaitForPlayersState _waitForPlayersState;
        private readonly GameState _gameState;

        public GameStateMachine(InitState initState,
                                WaitForPlayersState waitForPlayersState,
                                GameState gameState)
        {
            _initState = initState;
            _waitForPlayersState = waitForPlayersState;
            _gameState = gameState;
        }

        public void Initialize()
        {
            _stateMachine = new StateMachine<GameStates, GameTriggers>(GameStates.None);

            _stateMachine.Configure(GameStates.None)
                .Permit(GameTriggers.StartInit, GameStates.Init);
            
            _stateMachine.Configure(GameStates.Init)
                .Permit(GameTriggers.StartConnect, GameStates.WaitForPlayers)
                .OnEntry(_initState.OnEntry);

            _stateMachine.Configure(GameStates.WaitForPlayers)
                .Permit(GameTriggers.StartGame, GameStates.Game)
                .OnEntry(_waitForPlayersState.OnEntry)
                .OnExit(_waitForPlayersState.OnExit);

            _stateMachine.Configure(GameStates.Game)
                .Permit(GameTriggers.EndGame, GameStates.Init)
                .InternalTransition(GameTriggers.ShowResults, _gameState.ShowResults)
                .InternalTransition(GameTriggers.HideResults, _gameState.HideResults)
                .OnEntry(_gameState.OnEntry)
                .OnEntry(_gameState.OnExit);
            
            _stateMachine.Fire(GameTriggers.StartInit);
        }

        public void Fire(GameTriggers trigger)
        {
            _stateMachine.Fire(trigger);
        }
    }
}