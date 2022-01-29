using System;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players.Base;
using Photon.Pun;
using UniRx;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerTypeController : MonoBehaviour
    {
        public PhotonView PhotonView;
        public Animator Animator;
        
        public ObjectState Type;
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
        
        private void Awake()
        {
            if (PhotonView.IsMine)
            {
                _controlInfo.ChangeTypeButton.onClick.RemoveAllListeners();
                _controlInfo.ChangeTypeButton.onClick.AddListener(ChangeType);
            }
        }

        private void ChangeType()
        {
            if (_ready)
            {
                InvertState();
                PhotonView.RPC("ChangeTypeRemote", RpcTarget.All, Type);

                StartTimer();
                _ready = false;
            }
        }

        [PunRPC]
        private void ChangeTypeRemote(ObjectState state)
        {
            Type = state;
            SpriteRenderer.color = Type == ObjectState.First ? Color.white : Color.red;
        }
        
        private void InvertState()
        {
            Type = Type == ObjectState.First ? ObjectState.Second : ObjectState.First;
        }
    }
}