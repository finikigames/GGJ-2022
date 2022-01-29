using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2022.Source.Scripts.Controls
{
    public class JoystickControlInfo : MonoBehaviour
    {
        public Joystick MovementJoystick;
        public Joystick FireJoystick;
        public Button ChangeTypeButton;

        public Vector3 ShootDirection;
    }
}