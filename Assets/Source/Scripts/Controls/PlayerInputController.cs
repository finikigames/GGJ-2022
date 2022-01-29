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
        
        // Start is called before the first frame update
        void Start()
        {
        
        }
        
        void FixedUpdate()
        {
            Vector2 newPos = new Vector2(
                _joysticks.MovementJoystick.Horizontal * 100f + Input.GetAxis("Horizontal") * 100f,
                _joysticks.MovementJoystick.Vertical * 100f + Input.GetAxis("Vertical") * 100f);
            var move = Vector2.ClampMagnitude(newPos, 1) * _moveSpeed * Time.fixedDeltaTime + Rigidbody.position;
            Rigidbody.MovePosition(move);
        }
    }
}
