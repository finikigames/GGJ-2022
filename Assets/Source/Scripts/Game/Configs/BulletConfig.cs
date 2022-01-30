using System;
using GGJ2022.Source.Scripts.Game.Players.Base;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.Configs
{
    [Serializable]
    public class BulletConfig
    {
        public float BulletSpeed;
        public LayerMask DestroyBulletLayers;
        public Color FirstTeamProjectileColor;
        public Color SecondTeamProjectileColor;

        public Color GetRightColor(ObjectState state)
            => state == ObjectState.First ? FirstTeamProjectileColor : SecondTeamProjectileColor;
    }
}