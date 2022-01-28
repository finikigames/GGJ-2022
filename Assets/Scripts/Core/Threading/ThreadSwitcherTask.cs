﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Source.Scripts.Core.Threading.Interfaces;

namespace Source.Scripts.Core.Threading
{
    internal struct ThreadSwitcherTask : IThreadSwitcher
    {
        public IThreadSwitcher GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted => SynchronizationContext.Current == null;

        public void GetResult()
        {
        }

        public void OnCompleted([NotNull] Action continuation)
        {
            if (continuation == null)
                throw new ArgumentNullException(nameof(continuation));

            Task.Run(continuation);
        }
    }
}