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
                _controlInfo.FireJoystick.Horizontal * 100f + Input.GetAxis("Horizontal") * 100f,
                _controlInfo.FireJoystick.Vertical * 100f + Input.GetAxis("Vertical") * 100f);
            if (firePos.normalized.magnitude < 1 && _lineRenderer.enabled)
            {
                _lineRenderer.enabled = false;
                return;
            }

            if (!_lineRenderer.enabled)
            {
                _lineRenderer.enabled = true;
            }
            _lineRenderer.SetPosition(1, new Vector3(1, 1, 1 ) * firePos);
        }
    }
}