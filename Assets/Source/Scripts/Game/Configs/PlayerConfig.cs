using System;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.Configs
{
    [Serializable]
    public class PlayerConfig
    {
        public float MoveSpeed = 5;
        public float RotationSpeed;
        public float FireDistance = 6;
        public float ShootDelay = 1;
        public float StateSwitchDelay = 2;
        public float Health = 100;
        public float Damage = 20;
        public float EnemyHeal = 5;
        public float AllyHeal = 10;
        public float FriendlyFireDamage = 5;
    }
}