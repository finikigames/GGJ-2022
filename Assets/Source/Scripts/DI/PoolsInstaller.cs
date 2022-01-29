using ExitGames.Client.Photon;
using GGJ2022.Source.Scripts.Game;
using GGJ2022.Source.Scripts.Game.ECS.Pools;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.DI
{
    public class PoolsInstaller : MonoInstaller<PoolsInstaller>
    {
        public GameObject ProjectilePrefab;
        public override void InstallBindings()
        {
            /*Container.BindMemoryPool<Projectile, Projectile.ProjectilePool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(ProjectilePrefab)
                .UnderTransformGroup("Bullets"); */
        }
    }
}