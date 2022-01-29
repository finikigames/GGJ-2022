using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Pools
{
    public class PunPool : IPunPrefabPool
    {
        private List<GameObject> _used;
        private Queue<GameObject> _free;

        public PunPool()
        {
            _used = new List<GameObject>(20);
            _free = new Queue<GameObject>(20);
        }
        
        public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
        {
            var gameObject = _free.Count > 0 ? _free.Dequeue() : PhotonNetwork.Instantiate(prefabId, position, rotation);
            gameObject.SetActive(true);
            _used.Add(gameObject);

            return gameObject;
        }

        public void Destroy(GameObject gameObject)
        {
            _used.Remove(gameObject);
            _free.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }
}