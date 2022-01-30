using GGJ2022.Source.Scripts.Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedFilters;

namespace GGJ2022.Source.Scripts.Game.ECS.Systems
{
    public class BulletMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilterExt<ObjectViewRefData> _filterExt;
        
        public void Init(EcsSystems systems)
        {
            _filterExt.Validate(systems.GetWorld());
        }

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filterExt.Filter())
            {
                
            }
        }
    }
}