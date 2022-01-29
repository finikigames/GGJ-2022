using Photon.Pun;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Controls
{
    public class PlayerAimController : MonoBehaviour
    {
        private JoystickControlInfo _controlInfo;
        public PhotonView photonView;
        public LineRenderer _lineRenderer;
        
        [Inject]
        public void Construct(JoystickControlInfo controlInfo)
        {
            _controlInfo = controlInfo;
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
            linePos += (firePos.normalized * 5);
            _lineRenderer.SetPosition(1, linePos);
        }
    }
}