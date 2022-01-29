using Zenject;

namespace GGJ2022.Source.Scripts.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            StateInstaller.Install(Container);
            
            Container
                .Bind<Game.Services.Game>()
                .AsSingle();
        }
    }
}
