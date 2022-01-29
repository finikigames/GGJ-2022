using System;
using Photon.Pun;
using UnityEngine;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerLayerController : MonoBehaviour
    {
        public PhotonView PhotonView;
        public static string SelfLayer = "PlayerSelf";
        public static string OtherLayer = "PlayerOther";
        
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer(PhotonView.IsMine ? SelfLayer : OtherLayer);
        }
    }
}