using System;
using UnityEngine;

namespace Game.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        public GameObject PlayerPrefab;
        public float MoveSpeed;
        public float RotatinSpeed;
    }
}