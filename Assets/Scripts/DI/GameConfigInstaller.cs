using Game.Configs;
using UnityEngine;
using Zenject;

namespace DI
{
    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
    public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
    {
        public GameConfig GameConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameConfig>().FromInstance(GameConfig).AsSingle();
        }
    }
}