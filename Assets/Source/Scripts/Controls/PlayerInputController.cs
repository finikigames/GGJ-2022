using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PlayerInputController : MonoBehaviour
    {
        [Inject] private JoystickControlInfo _joysticks;
        public Rigidbody2D Rigidbody;
        //Temporary
        public float _moveSpeed = 25f;
        public float Horizontal;
        public float Vertical;
        public Vector2 Movement;
        
        private void FixedUpdate()
        {
            Horizontal = _joysticks.MovementJoystick.Horizontal * 100f + Input.GetAxis("Horizontal") * 100f;
            Vertical = _joysticks.MovementJoystick.Vertical * 100f + Input.GetAxis("Vertical") * 100f;
            Vector2 newPos = new Vector2(
                Horizontal,
                Vertical);
            Movement = Vector2.ClampMagnitude(newPos, 1);
            Rigidbody.MovePosition(Movement * _moveSpeed * Time.fixedDeltaTime + Rigidbody.position);
        }
    }
}
