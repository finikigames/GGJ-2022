using System.Collections.Generic;
using GGJ2022.Source.Scripts.Game;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerAimController : MonoBehaviour
    {
        private JoystickControlInfo _controlInfo;
        private PlayerConfig _playerConfig;
        private GameScope _gameScope;
        
        public PhotonView photonView;
        public LineRenderer _lineRenderer;
        public ContactFilter2D ContactFilter2D;

        [Inject]
        public void Construct(JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig,
                              GameScope gameScope)
        {
            _controlInfo = controlInfo;
            _playerConfig = playerConfig;
        }

        void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                AimTrajectory();
            }
        }

        private void AimTrajectory()
        {
            if (_controlInfo.FireJoystick.Direction.magnitude < 0.5f)
            {
                if (!_lineRenderer.enabled) return;
                _lineRenderer.enabled = false; 
                return;
            }

            if (!_lineRenderer.enabled)
            {
                _lineRenderer.enabled = true;
            }
            
            var heroPosition = new Vector2(transform.position.x, transform.position.y);

            _lineRenderer.SetPosition(0, heroPosition);
            var linePos = new Vector2(heroPosition.x, heroPosition.y);
            linePos += _controlInfo.FireJoystick.Direction * _playerConfig.FireDistance;
            List<RaycastHit2D> results = new List<RaycastHit2D>(1);
            var hitsCount = Physics2D.Raycast(heroPosition, _controlInfo.FireJoystick.Direction, ContactFilter2D, results);
            
            if (hitsCount > 0)
            {
                var hit = results[0];

                if (hit.distance < _playerConfig.FireDistance)
                {
                    //linePos = hit.point;

                    linePos = new Vector2(heroPosition.x, heroPosition.y);
                    linePos += (_controlInfo.FireJoystick.Direction * hit.distance);
                }
            }

            _lineRenderer.SetPosition(1, linePos);

            _controlInfo.ShootDirection = (linePos - (Vector2)heroPosition).normalized;
        }
    }
}