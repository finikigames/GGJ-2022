using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Source.Scripts.Core.Utils
{
    public struct ReadOnlyNativeWrappedData<T> where T : unmanaged
    {
        [ReadOnly] 
        [NativeDisableParallelForRestriction]
        public NativeArray<T> Array;
#if UNITY_EDITOR
        public AtomicSafetyHandle SafetyHandle;
#endif
    }
}