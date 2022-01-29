using GGJ2022.Source.Scripts.Game.Configs;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.DI.ScriptableInstallers
{
    [CreateAssetMenu(fileName = "BulletConfigInstaller", menuName = "Installers/BulletConfigInstaller")]
    public class BulletConfigInstaller : ScriptableObjectInstaller<BulletConfigInstaller>
    {
        public BulletConfig BulletConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(BulletConfig)
                .AsSingle();
        }
    }
}