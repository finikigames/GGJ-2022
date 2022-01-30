using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.ECS.Pools
{
    public class Projectile : MonoBehaviour
    {
        public class ProjectilePool : MonoMemoryPool<Projectile>
        {
        
        }
    }
}