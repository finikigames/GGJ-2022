using System;
using System.Collections.Generic;
using Source.Scripts.Core.Pools;
using UnityEditor;

namespace Source.Scripts.Core.Utils
{
#if UNITY_EDITOR
    public static class RichPoolRegistry
    {
        public static event Action<GameObjectsPool> PoolAdded = delegate { };

        public static event Action<GameObjectsPool> PoolRemoved = delegate { };
        
        private static readonly List<GameObjectsPool> _pools = new List<GameObjectsPool>();

        static RichPoolRegistry()
        {
            EditorApplication.playModeStateChanged += change => _pools.Clear();
        }
        
        public static IEnumerable<GameObjectsPool> Pools => _pools;

        public static void Add(GameObjectsPool memoryPool)
        {
            _pools.Add(memoryPool);
            PoolAdded(memoryPool);
        }

        public static void Remove(GameObjectsPool memoryPool)
        {
            _pools.Remove(memoryPool);
            PoolRemoved(memoryPool);
        }
    }
#endif
}