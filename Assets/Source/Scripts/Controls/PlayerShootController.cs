using System;
using GGJ2022.Source.Scripts.Game.Bullets;
using GGJ2022.Source.Scripts.Game.Configs;
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
        private IDisposable _timer;
        private bool _ready = true;
        private PlayerConfig _playerConfig;


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

        private void Awake()
        {
            if (PhotonView.IsMine)
            {
                _controlInfo.FireJoystick.IsPointerUp
                    .Where(x => x && _controlInfo.ShootDirection.magnitude > _gameConfig.FireStickTreeshold && _ready).Subscribe(
                        _ =>
                        {
                            Shoot();
                            _ready = false;
                            StartTimer();
                        });
            }
        }
        
        private void StartTimer()
        {
            _timer = Observable.Interval(TimeSpan.FromSeconds(_playerConfig.ShootDelay)).Subscribe(_ =>
            {
                _ready = true;
            });
        }


        private void Shoot()
        {
            var currentType = PlayerTypeController.Type;
           
            var bullet = PhotonNetwork.Instantiate(_gameConfig.BulletPrefab.name, transform.position, quaternion.identity);
            var projectileController = bullet.GetComponent<ProjectileController>();
            projectileController.Initialize(currentType);
        }
    }
}