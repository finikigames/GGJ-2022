using System;
using GGJ2022.Source.Scripts.Controls;
using GGJ2022.Source.Scripts.Game.Configs;
using Photon.Pun;
using UniRx;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Controls
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Inject]
        private JoystickControlInfo _joysticks;
        [Inject]
        private GameConfig _gameConfig;
        [Inject]
        private PlayerConfig _playerConfig;

        private bool _ready = true;
        private IDisposable _timer;

        public PhotonView PhotonView;
        public Animator Animator;

        //For idleState
        private int _prevAnimation;
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int PrevHorizontal = Animator.StringToHash("PrevHorizontal");
        private static readonly int PrevVertical = Animator.StringToHash("PrevVertical");
        private static readonly int Attack = Animator.StringToHash("Attack");

        private void Awake()
        {
            if (PhotonView.IsMine)
            {
                _joysticks.FireJoystick.IsPointerUp
                    .Where(x => x && _joysticks.ShootDirection.magnitude > _gameConfig.FireStickTreeshold && _ready).Subscribe(
                        _ =>
                        {
                            AttackAnimation();
                            _ready = false;
                            StartTimer();
                        });
            }
        }

        private void FixedUpdate()
        {
            if (PhotonView.IsMine)
            {
                Animate();
            }
        }

        private void Animate()
        {
            var horizontal = _joysticks.MovementJoystick.Horizontal * 100f + Input.GetAxis("Horizontal") * 100f;
            var vertical = _joysticks.MovementJoystick.Vertical * 100f + Input.GetAxis("Vertical") * 100f;
            Vector2 newPos = new Vector2(
                horizontal,
                vertical);
            var move = Vector2.ClampMagnitude(newPos, 1);
            FillAnimation(horizontal, vertical, move);
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

        private void StartTimer()
        {
            _timer = Observable.Interval(TimeSpan.FromSeconds(_playerConfig.ShootDelay)).Subscribe(_ =>
            {
                _ready = true;
            });
        }

        private void AttackAnimation()
        {
            Animator.SetTrigger(Attack);
        }
    }
}