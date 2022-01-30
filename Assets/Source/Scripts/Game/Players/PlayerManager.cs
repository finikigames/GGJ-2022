// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerManager.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in PUN Basics Tutorial to deal with the networked player instance
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.Services;
using GGJ2022.Source.Scripts.UI;
using GGJ2022.Source.Scripts.UI.Player;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun.UtilityScripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Players
{
	#pragma warning disable 649

    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields

        public PhotonView PhotonView;
        public SpriteRenderer TeamCircle;
        private PlayerConfig _playerConfig;
        private PlayerService _playerService;
        private GameConfig _gameConfig;
        private HealthBar _healthBar;
        private CooldownBar _cooldownBar;
        private int _myTeam;
        private TextMeshProUGUI _nickname;

        [Inject]
        public void Construct(PlayerConfig playerConfig,
                              PlayerService playerService,
                              GameConfig gameConfig,
                              GameScope gameScope)
        {
            _playerConfig = playerConfig;
            _playerService = playerService;
            _gameConfig = gameConfig;
            _gameScope = gameScope;
        }

        [Tooltip("The current Health of our player")]
        public float _health;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        private bool _isLeft = false;
        
        #region Private Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab;

        private GameScope _gameScope;

        public void Awake()
        {
            if (PhotonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
                _health = _playerConfig.Health;
            }

            // Create the UI
            if (this.playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(this.playerUiPrefab, transform);
                _healthBar = _uiGo.GetComponentInChildren<HealthBar>();
                _nickname = _uiGo.GetComponentInChildren<TextMeshProUGUI>();
                _cooldownBar = _uiGo.GetComponentInChildren<CooldownBar>();
                _healthBar.InitializeSlider(_health);

                if (PhotonView.IsMine)
                {
                    _nickname.text = _gameScope.Nickname;

                    PhotonView.RPC("SetNicknames", RpcTarget.Others, _gameScope.Nickname);
                }
                else
                {
                    _cooldownBar.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
            if (PhotonView.IsMine)
            {
                //FillCircleTeam();
            }
            
            DontDestroyOnLoad(gameObject);
        }

        [PunRPC]
        private void SetNicknames(string nickname)
        {
            _nickname.text = nickname;
        }

        public void SetCooldown(float cooldown)
        {
            _cooldownBar.Set(cooldown);
        }
        
        public void Heal(float heal)
        {
            if (_health + heal < 100)
            {
                _health += heal;
            }
            else
            {
                _health = 100;
            }
            
            if (!PhotonView.IsMine)
            {
                PhotonView.RPC("RPC_CurrentHealth", RpcTarget.Others, _health);
            }
            
            _healthBar.SetHealth(_health);
        }

        public void Damage(float damage)
        {
            _health -= damage;
            _healthBar.SetHealth(_health);
            
            if (!PhotonView.IsMine)
            {
                PhotonView.RPC("RPC_CurrentHealth", RpcTarget.Others, _health);
            }
        }

        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }

            GameObject _uiGo = Instantiate(this.playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        #endregion

        #region Private Methods

#if UNITY_5_4_OR_NEWER


        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
		{
			this.CalledOnLevelWasLoaded(scene.buildIndex);
		}

#endif

        #endregion

        public override void OnLeftRoom()
        {
            _isLeft = true;
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_health);
            }
            else
            {
                _health = (float)stream.ReceiveNext();
            }
        }

        void CheckHealth()
        {
            if (PhotonView.IsMine && !_isLeft && _health <= 0f)
            {
                PhotonNetwork.LeaveRoom();
            }
        }

        [PunRPC]
        void RPC_CurrentHealth(float whichHealth)
        {
            _health = whichHealth;
            _healthBar.SetHealth(_health);
            CheckHealth();
        }

        private void FillCircleTeam()
        {
            if (_gameConfig.isDeathmatch) return;
            var team = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code;
            if (team == 1)
            {
                TeamCircle.enabled = true;
                TeamCircle.color = new Color(131, 255, 5, 80);
            }

            if (team == 2)
            {
                TeamCircle.enabled = true;
                TeamCircle.color = new Color(0, 255, 237, 80);
            }
            PhotonView.RPC("RPC_TeamCircle", RpcTarget.Others, TeamCircle);
        }

        [PunRPC]
        void RPC_TeamCircle(SpriteRenderer circle)
        {
            TeamCircle = circle;
        }
    }
}