using System;
using DG.Tweening;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GGJ2022.Source.Scripts.UI
{
    public class CanvasGroupHider : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        public Image CloseButton;
        
        private GameConfig _gameConfig;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameConfig gameConfig,
                              GameStateMachine gameStateMachine)
        {
            _gameConfig = gameConfig;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            CanvasGroup.DOFade(1f, _gameConfig.PlayerStartHintHideTime).onComplete += () =>
            {
                CloseButton.DOFade(1f, _gameConfig.CloseButtonHideTime);
            };
        }

        public void Hide()
        {
            CanvasGroup.DOFade(0f, _gameConfig.PlayerStartHintHideTime).onComplete += () =>
            {
                CanvasGroup.gameObject.SetActive(false);
                _gameStateMachine.Start();
            };
        }
    }
}
