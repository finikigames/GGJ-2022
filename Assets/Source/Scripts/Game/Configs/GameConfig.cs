using System;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.Configs
{
    [Serializable]
    public class GameConfig
    {
        public int PlayersToStartGame = 4;
        public GameObject PlayerPrefab;
        public GameObject BulletPrefab;
        public bool IsEnemyHeal;
        public bool FriendlyFire;
        public float FireStickTreeshold;
        public bool isDeathmatch;

        public float WinScreenShowTime;
        
        public float PlayerStartHintHideTime;
        public float CloseButtonHideTime;
    }
}