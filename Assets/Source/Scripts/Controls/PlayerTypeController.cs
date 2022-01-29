using System;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players.Base;
using Photon.Pun;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerTypeController : MonoBehaviour
    {
        public PhotonView PhotonView;
        public Animator Animator;
        
        public ObjectState State;
        public AnimatorController FirstStateView;
        public AnimatorController SecondStateView;

        public SpriteRenderer SpriteRenderer;

        private JoystickControlInfo _controlInfo;
        private PlayerConfig _playerConfig;

        [Inject]
        public void Construct(JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig)
        {
            _controlInfo = controlInfo;
            _playerConfig = playerConfig;
        }

        private void Start()
        {
            if (PhotonView.IsMine)
            {
                _controlInfo.ChangeTypeButton.onClick.AddListener(ChangeType);
            }
        }

        private void ChangeType()
        {
            InvertState();
            PhotonView.RPC("ChangeTypeRemote", RpcTarget.All, State);
        }

        [PunRPC]
        private void ChangeTypeRemote(ObjectState state)
        {
            State = state;
            SpriteRenderer.color = State == ObjectState.First ? Color.white : Color.red;
        }
        
        private void InvertState()
        {
            State = State == ObjectState.First ? ObjectState.Second : ObjectState.First;
        }
    }
}