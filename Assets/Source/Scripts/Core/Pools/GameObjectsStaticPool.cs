using Source.Scripts.Core.Pools.Base;
using UnityEngine;

namespace Source.Scripts.Core.Pools
{
    public class GameObjectsStaticPool : BaseGamePool
    {
        public GameObjectsStaticPool(GameObject go,
                                     int size) : base(go, size)
        {
            
        }
    }
}