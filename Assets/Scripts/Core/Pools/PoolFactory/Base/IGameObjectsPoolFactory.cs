using Source.Scripts.Core.Pools.Base;
using UnityEngine;

namespace Source.Scripts.Core.Pools.PoolFactory.Base
{
    public interface IGameObjectsPoolFactory<in T>
    {
        IPool Create(T id, GameObject gameObject, int size);
    }
}