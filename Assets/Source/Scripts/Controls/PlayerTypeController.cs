using System;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players.Base;
using Photon.Pun;
using UniRx;
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
        
        private IDisposable _timer;
        private bool _ready = true;
        
        
        [Inject]
        public void Construct(JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig)
        {
            _controlInfo = controlInfo;
            _playerConfig = playerConfig;
        }

        private void StartTimer()
        {
            _timer = Observable.Interval(TimeSpan.FromSeconds(_playerConfig.StateSwitchDelay)).Subscribe(_ =>
            {
                _ready = true;
            });
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
            if (_ready)
            {
                InvertState();
                PhotonView.RPC("ChangeTypeRemote", RpcTarget.All, State);

                StartTimer();
                _ready = false;
            }
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