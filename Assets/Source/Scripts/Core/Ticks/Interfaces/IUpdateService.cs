using Zenject;

namespace Source.Scripts.Core.Ticks.Interfaces
{
    public interface IUpdateService
    {
        void RegisterUpdate(IUpdatable updatable);
        void RegisterFixedUpdate(IFixedUpdateRunner fixedUpdateRunner);
        void UnregisterUpdate(IUpdatable updatable);
        void UnregisterFixedUpdate(IFixedUpdateRunner fixedUpdateRunner);
    }
}