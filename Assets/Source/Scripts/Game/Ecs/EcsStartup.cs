using System.Collections.Generic;
using Leopotam.EcsLite;
using Source.Scripts.Core.Ticks;
using Source.Scripts.Core.Ticks.Interfaces;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.ECS
{
    public class EcsStartup : IUpdatable
    {
        public EcsWorld World = new();
        private List<EcsSystems> systems; 
        
        [Inject] 
        private UpdateService _updateService;

        public void RegisterRunner()
        {
            _updateService.RegisterUpdate(this);
        }

        public void UnRegisterRunner()
        {
            _updateService.UnregisterUpdate(this);
        }

        public void CustomUpdate()
        {
            
        }
    }
}