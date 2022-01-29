using System;
using Controls;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Controls
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Inject] private JoystickControlInfo _joysticks;

        public PlayerInputController Controller;
        public Animator Animator;
        //For idleState
        private int _prevAnimation;
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int PrevHorizontal = Animator.StringToHash("PrevHorizontal");
        private static readonly int PrevVertical = Animator.StringToHash("PrevVertical");

        private void FixedUpdate()
        {
            FillAnimation(Controller.Horizontal, Controller.Vertical, Controller.Movement);
        }

        private void FillAnimation(float horizontal, float vertical, Vector2 move)
        {
            Animator.SetFloat(Movement, move.normalized.magnitude);
            Animator.SetFloat(Horizontal, horizontal);
            Animator.SetFloat(Vertical, vertical);
            
            var clipName = Animator.StringToHash(Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            if (_prevAnimation != clipName && move.normalized.magnitude > 0.3f)
            {
                _prevAnimation = clipName;
                Animator.SetFloat(PrevHorizontal, horizontal);
                Animator.SetFloat(PrevVertical, vertical);
            }
        }
    }
}