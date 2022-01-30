using GGJ2022.Source.Scripts.Game.Configs;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.DI.ScriptableInstallers
{
    [CreateAssetMenu(fileName = "PlayerConfigInstaller", menuName = "Installers/PlayerConfigInstaller")]
    public class PlayerConfigInstaller : ScriptableObjectInstaller<PlayerConfigInstaller>
    {
        public PlayerConfig PlayerConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(PlayerConfig)
                .AsSingle();
        }
    }
}