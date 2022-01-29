using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Core.Ticks.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core.Ticks
{
    public class UpdateService : MonoBehaviour, IUpdateService
    {
        private readonly List<IUpdatable> _updates;

        private readonly List<IFixedUpdateRunner> _fixedUpdates;

        public UpdateService()
        {
            _updates = new List<IUpdatable>();
            _fixedUpdates = new List<IFixedUpdateRunner>();
        }
        
        void Update()
        {
            for (int i = _updates.Count - 1; i >= 0; i--) 
            {
                var runner = _updates[i];
                
                runner.CustomUpdate();
            }
        }

        void FixedUpdate()
        {
            for (int i = _fixedUpdates.Count - 1; i >= 0; i--) 
            {
                var runner = _fixedUpdates[i];
                
                runner.CustomFixedUpdate();
            }
        }
    
        public void RegisterUpdate(IUpdatable updatable) 
        {
            if (!_updates.Contains(updatable))
            {
                _updates.Add(updatable);
            }
        }

        public void RegisterFixedUpdate(IFixedUpdateRunner fixedUpdateRunner)
        {
            if (!_fixedUpdates.Contains(fixedUpdateRunner))
            {
                _fixedUpdates.Add(fixedUpdateRunner);
            }
        }

        public void UnregisterUpdate(IUpdatable updatable)
        {
            if (_updates.Contains(updatable))
            {
                _updates.Remove(updatable);
            }
        }

        public void UnregisterFixedUpdate(IFixedUpdateRunner fixedUpdateRunner)
        {
            if (_fixedUpdates.Contains(fixedUpdateRunner))
            {
                _fixedUpdates.Remove(fixedUpdateRunner);
            }
        }
    }
}