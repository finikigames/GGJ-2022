using System;
using System.Collections;
using GGJ2022.Source.Scripts.Game.Bullets;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players;
using GGJ2022.Source.Scripts.Game.Players.Base;
using Photon.Pun;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerShootController : MonoBehaviour
    {
        private BulletConfig _bulletConfig;
        private JoystickControlInfo _controlInfo;
        private GameConfig _gameConfig;

        public PhotonView PhotonView;
        public PlayerTypeController PlayerTypeController;
        public PlayerManager PlayerManager;
        
        private bool _ready = true;
        private PlayerConfig _playerConfig;
        private ReactiveProperty<float> _timerValue;
        
        [Inject]
        public void Construct(BulletConfig bulletConfig,
                              JoystickControlInfo controlInfo,
                              GameConfig gameConfig,
                              PlayerConfig playerConfig)
        {
            _bulletConfig = bulletConfig;
            _controlInfo = controlInfo;
            _gameConfig = gameConfig;
            _playerConfig = playerConfig;
        }

        private void Start()
        {
            if (PhotonView.IsMine)
            {
                _timerValue = new ReactiveProperty<float>(_playerConfig.ShootDelay);
                _timerValue.Subscribe(value =>
                {
                    PlayerManager.SetCooldown(value);
                });
                
                _controlInfo.FireJoystick.IsPointerUp
                    .Where(x => x && _controlInfo.ShootDirection.magnitude > _gameConfig.FireStickTreeshold && _ready).Subscribe(
                        _ =>
                        {
                            _timerValue.Value = 0f;
                            Shoot();
                            _ready = false;
                            StartCoroutine(StartTimer());
                        });
            }
        }
        
        IEnumerator StartTimer()
        {
            while (_timerValue.Value < _playerConfig.ShootDelay)
            {
                _timerValue.Value += Time.deltaTime;
                yield return null;
            }
            
            _ready = true;
        }


        private void Shoot()
        {
            var currentType = PlayerTypeController.Type;
            if (currentType == ObjectState.First)
            {
                PhotonView.RPC("PlayAngelShootSound", RpcTarget.All);
            }
            else
            {
                PhotonView.RPC("PlayDevilShootSound", RpcTarget.All);
            }

            var bullet = PhotonNetwork.Instantiate(_gameConfig.BulletPrefab.name, transform.position, quaternion.identity);
            var projectileController = bullet.GetComponent<ProjectileController>();
            projectileController.Initialize(currentType);
        }
    }
}