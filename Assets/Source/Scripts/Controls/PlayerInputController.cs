using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerInputController : MonoBehaviour
    {
        private JoystickControlInfo _controlInfo;
        private PlayerConfig _playerConfig;
        
        [Inject]
        public void Construct(JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig)
        {
            _controlInfo = controlInfo;
            _playerConfig = playerConfig;
        }

        public Rigidbody2D Rigidbody;
        public PhotonView photonView;

        void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                Move();
            }
        }

        private void Move()
        {
            Vector2 newPos = new Vector2(
                _controlInfo.MovementJoystick.Horizontal * 100f + Input.GetAxis("Horizontal") * 100f,
                _controlInfo.MovementJoystick.Vertical * 100f + Input.GetAxis("Vertical") * 100f);
            var move = Vector2.ClampMagnitude(newPos, 1) * _playerConfig.MoveSpeed * Time.fixedDeltaTime + Rigidbody.position;
            Rigidbody.MovePosition(move);
        }
    }
}
