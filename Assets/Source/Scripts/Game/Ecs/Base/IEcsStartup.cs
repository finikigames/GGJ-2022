using System;

namespace GGJ2022.Source.Scripts.Game.ECS.Base
{
    public interface IEcsStartup : IDisposable
    {
        void Initialize();
    }
}