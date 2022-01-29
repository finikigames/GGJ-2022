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
        
        public PhotonView photonView;
        public LineRenderer _lineRenderer;
        public GameObject Raycast;
        private float _distanceObj;
        
        [Inject]
        public void Construct(JoystickControlInfo controlInfo,
                              PlayerConfig playerConfig)
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
            Vector2 firePos = new Vector2(
                _controlInfo.FireJoystick.Horizontal * 100f,
                _controlInfo.FireJoystick.Vertical * 100f);
            if (firePos.normalized.magnitude < 0.5f)
            {
                if (!_lineRenderer.enabled) return;
                _lineRenderer.enabled = false; 
                return;
            }

            if (!_lineRenderer.enabled)
            {
                _lineRenderer.enabled = true;
            }
            
            var position = transform.position;
            _lineRenderer.SetPosition(0, position);
            var linePos = new Vector2(position.x, position.y);
            RaycastHit hit;
            if (Physics.Raycast(Raycast.transform.position, Raycast.transform.forward, out hit))
            {
                if (hit.collider != null)
                {
                    transform.localPosition = new Vector3(1f, 1f, _distanceObj);
                }
                _distanceObj = hit.distance;
            }
            
            linePos += (firePos.normalized * _playerConfig.FireDistance);
            _lineRenderer.SetPosition(1, linePos);
        }
    }
}