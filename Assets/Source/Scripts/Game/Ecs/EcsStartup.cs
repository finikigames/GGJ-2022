using GGJ2022.Source.Scripts.Game.ECS.Base;
using GGJ2022.Source.Scripts.Game.ECS.Systems;
using Leopotam.EcsLite;
using Source.Scripts.Core.Ticks;
using Source.Scripts.Core.Ticks.Interfaces;
using Voody.UniLeo.Lite;

namespace GGJ2022.Source.Scripts.Game.ECS
{
    public class EcsStartup : IUpdatable, IEcsStartup
    {
        private UpdateService _updateService;

        private EcsWorld _world;
        private EcsSystems _systems;
        private readonly GameScope _gameScope;

        public EcsStartup(UpdateService updateService,
                          GameScope gameScope)
        {
            _updateService = updateService;
            _gameScope = gameScope;
        }

        public void Initialize()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .ConvertScene()
                .Add(new BulletCreateSystem())
                .Add(new BulletMovementSystem());
            _gameScope.World = _world;
        }

        public void CustomUpdate()
        {
            _systems.Run();
        }

        public void RegisterRunner()
        {
            _updateService.RegisterUpdate(this);
        }

        public void UnRegisterRunner()
        {
            _updateService.UnregisterUpdate(this);
        }

        public void Dispose()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}