using System;
using GGJ2022.Source.Scripts.Game.Bullets;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
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
        
        
        [Inject]
        public void Construct(BulletConfig bulletConfig,
                              JoystickControlInfo controlInfo,
                              GameConfig gameConfig)
        {
            _bulletConfig = bulletConfig;
            _controlInfo = controlInfo;
            _gameConfig = gameConfig;
        }

        private void Awake()
        {
            if (PhotonView.IsMine)
            {
                _controlInfo.FireJoystick.IsPointerUp
                    .Where(x => x && _controlInfo.ShootDirection.magnitude > _gameConfig.FireStickTreeshold).Subscribe(
                        _ => { Shoot(); });
            }
        }

        private void Shoot()
        {
            var currentType = PlayerTypeController.State;
            var playerId = PhotonView.Owner.UserId;
            
            var bullet = PhotonNetwork.Instantiate(_gameConfig.BulletPrefab.name, transform.position, quaternion.identity);
            var projectileController = bullet.GetComponent<ProjectileController>();
            

        }
    }
}