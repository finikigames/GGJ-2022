using System;
using UnityEngine;
using Zenject;

namespace GGJ2022.Source.Scripts.Game.Services.CameraResolve
{
    public class CanvasCameraResolve : MonoBehaviour
    {
        public Canvas canvas;
        
        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}