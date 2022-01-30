using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Core.Pools.Base
{
    public abstract class BaseGamePool : IPool
    {
        private readonly Queue<GameObject> _objects = new();

        private readonly List<GameObject> _taken = new();
        
        private readonly GameObject _prefab;
        private readonly int _countToExpand;
        private int _realSize;

        public int NumTotal => _realSize;

        public int NumActive => _taken.Count;

        public int NumInactive => NumTotal - NumActive;
        
        private Transform _parent;
        private Action<GameObject> _instantiateProcess = go => {};

        protected BaseGamePool(GameObject go, int size = 10, int countToExpand = 10)
        {
            _realSize = size;
            _prefab = go;
            _countToExpand = countToExpand;
        }

        public IPool SetParentContainer(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public IPool SetInstantiateProcess(Action<GameObject> process)
        {
            _instantiateProcess = process;
            return this;
        }

        public void Fill()
        {
            Instantiate(_realSize);
        }

        public async Task FillAsync()
        {
            await InstantiateAsync(_realSize);
        }

        public async Task<GameObject> FillAsyncAndResult()
        {
            await InstantiateAsync(_realSize);
            return _objects.Peek();
        }

        public T Take<T>()
        {
            var go = Take();
            go.SetActive(true);
            go.transform.SetParent(_parent);
            return go.GetComponent<T>();
        }

        public T GetFirst<T>()
        {
            CheckExpand();
            
            var go = _objects.Peek();
            _taken.Add(go);
            go.transform.SetParent(_parent);
            return go.GetComponent<T>();
        }

        public GameObject Take()
        {
            CheckExpand();

            var go = _objects.Dequeue();
            _taken.Add(go);
            go.SetActive(true);
            go.transform.SetParent(_parent);
            return go;
        }

        public void Release(UnityComponent obj)
        {
            var gameObject = obj.gameObject;
            gameObject.SetActive(false);
            _objects.Enqueue(gameObject);
            _taken.Remove(gameObject);
        }

        public void Dispose()
        {
            for (int i = 0; i <= _objects.Count; i++)
            {
                var obj = _objects.Dequeue();
                
                Object.Destroy(obj);
            }
            _taken.Clear();
            _objects.Clear();
        }

        #region Expand

        public void ExpandBy(int count)
        {
            Instantiate(count);
        }

        public async Task ExpandByAsync(int count)
        {
            await InstantiateAsync(count);
        }

        private void CheckExpand()
        {
            if (_objects.Count <= _countToExpand)
            {
                Instantiate(_realSize);
                _realSize *= 2;
            }
        }

        #endregion

        private void Instantiate(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var go = Object.Instantiate(_prefab, _parent, true);
                go.transform.position = new Vector3(99999, 99999, 0);
                _instantiateProcess(go);
                go.SetActive(false);
                _objects.Enqueue(go);
            }
        }

        private async Task InstantiateAsync(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var go = Object.Instantiate(_prefab, _parent, true);
                _instantiateProcess(go);
                go.SetActive(false);
                _objects.Enqueue(go);
            }
        }
    }
}