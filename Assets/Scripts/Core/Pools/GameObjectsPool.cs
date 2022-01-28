using Source.Scripts.Core.Pools.Base;
using UnityEngine;

namespace Source.Scripts.Core.Pools
{
    public class GameObjectsPool : BaseGamePool
    {
        public GameObjectsPool(GameObject go, 
                               int size = 10, 
                               int countToExpand = 10) : base(go, size, countToExpand)
        {
            
        }
    }
}