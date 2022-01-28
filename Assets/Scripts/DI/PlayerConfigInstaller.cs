using Game.Configs;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
    public class PlayerConfigInstaller : ScriptableObjectInstaller<PlayerConfigInstaller>
    {
        public PlayerConfig PlayerConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerConfig>().FromInstance(PlayerConfig).AsSingle();
        }
    }
}