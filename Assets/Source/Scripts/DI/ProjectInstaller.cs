using Source.Scripts.Core.Ticks;
using Zenject;

namespace GGJ2022.Source.Scripts.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            Container
                .Bind<Game.Services.Game>()
                .AsSingle();

            Container
                .BindInterfacesTo<UpdateService>()
                .AsSingle();
        }
    }
}
