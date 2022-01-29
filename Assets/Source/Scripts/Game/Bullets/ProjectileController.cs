using System;
using GGJ2022.Source.Scripts.Controls;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Bullets
{
    public class ProjectileController : MonoBehaviour
    {
        public PhotonView PhotonView;

        public string PlayerId;

        private Vector3 _direciton;
        private Vector3 _startPosition;
        private BulletConfig _bulletConfig;
        private PlayerConfig _playerConfig;

        [Inject]
        public void Construct(BulletConfig bulletConfig,
                              JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig)
        {
            _direciton = controlInfo.ShootDirection;
            _startPosition = transform.position;
            _bulletConfig = bulletConfig;
            _playerConfig = playerConfig;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (PhotonView.IsMine)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            var isDistanceExceed = Vector2.Distance(transform.position, _startPosition) > _playerConfig.FireDistance;
            if (PhotonView.IsMine &&  !isDistanceExceed)
            {
                transform.position += _direciton * _bulletConfig.BulletSpeed * Time.deltaTime;
            }

            if (PhotonView.IsMine && isDistanceExceed)
            {
                Destroy(gameObject);
            }
        }
    }
}