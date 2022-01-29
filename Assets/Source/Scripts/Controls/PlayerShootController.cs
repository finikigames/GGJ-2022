using System;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using UniRx;
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
            
        }
    }
}