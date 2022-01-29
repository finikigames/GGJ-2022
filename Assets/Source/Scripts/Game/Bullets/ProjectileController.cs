using System.Collections.Generic;
using GGJ2022.Source.Scripts.Controls;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Players;
using GGJ2022.Source.Scripts.Game.Players.Base;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Bullets
{
    public class ProjectileController : SerializedMonoBehaviour
    {
        public Dictionary<ObjectState, StateView> StateViews; 
        
        public PhotonView PhotonView;

        public Collider Collider;

        private Vector3 _direciton;
        private Vector3 _startPosition;
        private BulletConfig _bulletConfig;
        private PlayerConfig _playerConfig;

        private ObjectState BulletState;
        private PhotonTeamsManager _teamsManager;
        private GameConfig _gameConfig;

        private bool _isDead;
        private StateView _currentState;

        [Inject]
        public void Construct(BulletConfig bulletConfig,
                              JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig,
                              PhotonTeamsManager teamsManager,
                              GameConfig gameConfig)
        {
            _direciton = controlInfo.ShootDirection;
            _startPosition = transform.position;
            _bulletConfig = bulletConfig;
            _playerConfig = playerConfig;
            _teamsManager = teamsManager;
            _gameConfig = gameConfig;
        }

        public void Initialize(ObjectState initialState)
        {
            BulletState = initialState;
            _currentState ??= _currentState = StateViews[BulletState];
            _currentState.Attack.gameObject.SetActive(true);
            float angle = Mathf.Atan2(_direciton.y, _direciton.x) * Mathf.Rad2Deg;
            _currentState.Attack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (!PhotonView.IsMine)
            {
                Collider.enabled = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _currentState ??= _currentState = StateViews[BulletState];
            var isPlayerCollision = other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerController.OtherLayer);

            _currentState.Attack.gameObject.SetActive(false);
            _currentState.Hit.gameObject.SetActive(true);

            // If other player
            if (PhotonView.IsMine && isPlayerCollision)
            {
                var gameObjectOther = other.gameObject.GetComponent<PhotonView>();

                var playerTypeController = gameObjectOther.GetComponent<PlayerTypeController>();

                _teamsManager.TryGetTeamMatesOfPlayer(PhotonView.Controller, out var teamMates);

                var otherPlayer = gameObjectOther.Controller;

                var playerManager = gameObjectOther.GetComponent<PlayerManager>();
                var isBulletState = playerTypeController.Type == BulletState;
                var isNotBulletState = playerTypeController.Type != BulletState;
                // This is your ally
                if (teamMates.Length > 0 && teamMates[0].UserId == otherPlayer.UserId)
                {
                    // Heal
                    if (isBulletState)
                        playerManager.Heal(_playerConfig.AllyHeal);
                    else if (isNotBulletState && _gameConfig.FriendlyFire)
                        playerManager.Damage(_playerConfig.FriendlyFireDamage);
                }
                else
                {
                    if (isBulletState && _gameConfig.IsEnemyHeal)
                        playerManager.Heal(_playerConfig.EnemyHeal);
                    else if (isNotBulletState)
                        playerManager.Damage(_playerConfig.Damage);
                }

                _isDead = true;
                _currentState.Hit.AnimationState.Complete += entry =>
                {
                    PhotonNetwork.Destroy(gameObject);
                };
            }

            
            _isDead = true;
            _currentState.Hit.AnimationState.Complete += entry =>
            {
                PhotonNetwork.Destroy(gameObject);
            };
        }

        private void Update()
        {
            _currentState ??= _currentState = StateViews[BulletState];
            
            var isDistanceExceed = Vector2.Distance(transform.position, _startPosition) > _playerConfig.FireDistance;
            if (PhotonView.IsMine &&  !isDistanceExceed && !_isDead)
            {
                transform.position += _direciton * _bulletConfig.BulletSpeed * Time.deltaTime;
            }

            if (PhotonView.IsMine && isDistanceExceed)
            {
                _isDead = true;
                transform.gameObject.SetActive(false);
                
                _currentState.Hit.AnimationState.Complete += _ =>
                {
                    PhotonNetwork.Destroy(gameObject);
                };
            }
        }
    }
}