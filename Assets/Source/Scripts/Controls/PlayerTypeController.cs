using System;
using System.Collections;
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
        private ReactiveProperty<float> _timerValue;


        [Inject]
        public void Construct(JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig)
        {
            _controlInfo = controlInfo;
            _playerConfig = playerConfig;
        }

        private void Awake()
        {
            if (PhotonView.IsMine)
            {
                _timerValue = new ReactiveProperty<float>();
                
                _controlInfo.ChangeTypeButton.onClick.RemoveAllListeners();
                _controlInfo.ChangeTypeButton.onClick.AddListener(ChangeType);
                
                _timerValue.Subscribe(value =>
                {
                    if (value > 0f)
                    {
                        _controlInfo.TimerText.text = String.Format("{0:0.00}", value);   
                    }
                    else
                    {
                        _controlInfo.TimerText.gameObject.SetActive(false);
                        _controlInfo.ReadyText.gameObject.SetActive(true);
                    }
                });

            }
        }

        IEnumerator StartTimer()
        {
            _timerValue.Value = _playerConfig.StateSwitchDelay;
            while (_timerValue.Value > 0.01f)
            {
                _timerValue.Value -= Time.deltaTime;
                yield return null;
            }

            _controlInfo.TimerText.gameObject.SetActive(false);
            _controlInfo.ReadyText.gameObject.SetActive(true);
            _ready = true;
        }

        private void ChangeType()
        {
            if (_ready)
            {
                _controlInfo.TimerText.gameObject.SetActive(true);
                _controlInfo.ReadyText.gameObject.SetActive(false);
                            
                _timerValue.Value = _playerConfig.StateSwitchDelay;
                InvertState();
                PhotonView.RPC("ChangeTypeRemote", RpcTarget.All, Type);

                StartCoroutine(StartTimer());
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