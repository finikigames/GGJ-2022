using GGJ2022.Source.Scripts.Game.Configs;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.DI.ScriptableInstallers
{
    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
    public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
    {
        public GameConfig GameConfig;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(GameConfig)
                .AsSingle();
        }
    }
}