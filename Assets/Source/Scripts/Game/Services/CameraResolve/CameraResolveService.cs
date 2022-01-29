using Cinemachine;
using GGJ2022.Source.Scripts.Game.Services.CameraResolve.Base;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Services.CameraResolve
{
    public class CameraResolveService : MonoBehaviour, ICameraResolveService
    {
        private GameScope _gameScope;
        
        public CinemachineVirtualCamera Cam;

        [Inject]
        public void Construct(GameScope gameScope)
        {
            _gameScope = gameScope;
        }

        public void Resolve()
        {
            Cam.Follow = _gameScope.LocalPlayer.transform;
        }
    }
}