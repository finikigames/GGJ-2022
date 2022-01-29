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
using GGJ2022.Source.Scripts.UI.Player;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
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
        private PlayerConfig _playerConfig;
        private PlayerService _playerService;
        private HealthBar _healthBar;
        private int _myTeam;

        [Inject]
        public void Construct(PlayerConfig playerConfig,
                              PlayerService playerService)
        {
            _playerConfig = playerConfig;
            _playerService = playerService;
        }

        [Tooltip("The current Health of our player")]
        public float _health;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        #endregion

        #region Private Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab;

        //True, when the user is firing
        bool IsFiring;

        public void Awake()
        {
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
                _health = _playerConfig.Health;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            // Create the UI
            if (this.playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(this.playerUiPrefab, transform);
                _healthBar = _uiGo.GetComponentInChildren<HealthBar>();
                _healthBar.InitializeSlider(_health);
            }
            else
            {
                Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
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
            
            if (!photonView.IsMine)
            {
                photonView.RPC("RPC_CurrentHealth", RpcTarget.Others, _health);
            }
            
            _healthBar.SetHealth(_health);
        }

        public void Damage(float damage)
        {
            _health -= damage;
            _healthBar.SetHealth(_health);
            
            if (!photonView.IsMine)
            {
                photonView.RPC("RPC_CurrentHealth", RpcTarget.Others, _health);
                
                if (_health <= 0f)
                {
                    PhotonNetwork.LeaveRoom();
                }
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

        private void Update()
        {
            
        }

        [PunRPC]
        void RPC_CurrentHealth(float whichHealth)
        {
            _health = whichHealth;
            _healthBar.SetHealth(_health);
        }
    }
}