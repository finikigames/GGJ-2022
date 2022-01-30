using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Core.Pools.Base
{
    public interface IPool
    {
        int NumTotal { get; }
        int NumActive { get; }
        int NumInactive { get; }

        // Pool settings
        IPool SetParentContainer(Transform poolContainer);
        IPool SetInstantiateProcess(Action<GameObject> process);
        
        // Pool usage
        void Release(UnityComponent go);
        Task<GameObject> FillAsyncAndResult();

        T Take<T>();
        
        void Dispose();
    }
}