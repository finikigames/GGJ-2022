using System;
using System.Collections;
using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players.Base;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Spine.Unity;
using UniRx;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerTypeController : MonoBehaviour
    {
        public PhotonView PhotonView;
        public Animator Animator;
        public SkeletonAnimation SwitchState;
        
        public ObjectState Type;
        public SpriteRenderer SpriteRenderer;

        public List<AnimatorController> AnimatorStates;

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
            _controlInfo.ChangeTypeButton.targetGraphic.color = Color.white;
            _ready = true;
        }

        private void ChangeType()
        {
            if (_ready)
            {
                _controlInfo.TimerText.gameObject.SetActive(true);
                _controlInfo.ReadyText.gameObject.SetActive(false);
                _controlInfo.ChangeTypeButton.targetGraphic.color = Color.gray;
                            
                _timerValue.Value = _playerConfig.StateSwitchDelay;
                //Animator.runtimeAnimatorController = InvertState();
                PhotonView.RPC("ChangeTypeRemote", RpcTarget.All, Type);

                StartCoroutine(StartTimer());
                _ready = false;
            }
        }

        [PunRPC]
        private void ChangeTypeRemote(ObjectState state)
        {
            Type = state;
            SwitchState.gameObject.SetActive(true);
            SwitchState.state.Complete += entry =>
            {
                SwitchState.gameObject.SetActive(false);
            };
            //SpriteRenderer.color = Type == ObjectState.First ? Color.white : Color.red;
            Animator.runtimeAnimatorController = InvertState();
        }
        
        private RuntimeAnimatorController InvertState()
        {
            Type = Type == ObjectState.First ? ObjectState.Second : ObjectState.First;
            switch (Type)
            {
                case ObjectState.First:
                    return ChangeToDemon();
                case ObjectState.Second:
                    return ChangeToAngel();
            }

            return null;
        }

        private RuntimeAnimatorController ChangeToDemon()
        {
            return AnimatorStates[Random.Range(2, 4)];
            if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1)
            {
                return AnimatorStates[2];
            }

            return AnimatorStates[3];
        }

        private RuntimeAnimatorController ChangeToAngel()
        {
            return AnimatorStates[Random.Range(0, 2)];

            if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1)
            {
                return AnimatorStates[0];
            }

            return AnimatorStates[1];
        }
    }
}