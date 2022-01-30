using System;
using DG.Tweening;
using GGJ2022.Source.Scripts.Game;
using GGJ2022.Source.Scripts.Game.Configs;
using GGJ2022.Source.Scripts.Game.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GGJ2022.Source.Scripts.UI
{
    public class CanvasGroupHider : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;
        public Image CloseButton;
        public TMP_InputField NicknameField;
        
        private GameConfig _gameConfig;
        private GameStateMachine _gameStateMachine;
        private GameScope _gameScope;

        [Inject]
        public void Construct(GameConfig gameConfig,
                              GameStateMachine gameStateMachine,
                              GameScope gameScope)
        {
            _gameConfig = gameConfig;
            _gameStateMachine = gameStateMachine;
            _gameScope = gameScope;
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
                if (NicknameField.text.Length > 0)
                {
                    _gameScope.Nickname = NicknameField.text;
                    CanvasGroup.gameObject.SetActive(false);
                    _gameStateMachine.Start();
                }
            };
        }
    }
}
