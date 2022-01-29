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
            
        }
    }
}