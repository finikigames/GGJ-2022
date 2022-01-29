using System;
using Photon.Pun;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Game.Bullets
{
    public class ProjectileController : MonoBehaviour
    {
        public PhotonView PhotonView;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
        }
    }
}