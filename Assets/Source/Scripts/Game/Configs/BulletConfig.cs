using System;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.Configs
{
    [Serializable]
    public class BulletConfig
    {
        public float BulletSpeed;
        public LayerMask DestroyBulletLayers;
    }
}