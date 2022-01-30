using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Source.Scripts.Core.Extensions
{
    public static class NativeCollectionsExtensions
    {
        public static NativeHashMap<TKey, TValue> Zip<TKey, TValue>(FixedList32Bytes<TKey> keys,
            FixedList32Bytes<TValue> values, Allocator allocator = Allocator.Temp)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            int length;
            if (keys.Length != values.Length)
            {
                length = keys.Length > values.Length ? values.Length : keys.Length;
            }
            else
            {
                length = keys.Length;
            }

            var nativeHashMap = new NativeHashMap<TKey, TValue>(length, allocator);

            for (int i = 0; i < length; i++)
            {
                nativeHashMap.Add(keys[i], values[i]);
            }

            return nativeHashMap;
        }

        // Use this instead of NativeHashMap<TKey, TValue> in parallel jobs
        public struct KeyToValueIndexMapper<TKey, TValue> : IDisposable
            where TKey : unmanaged
            where TValue : unmanaged
        {
            [NativeDisableParallelForRestriction]
            private NativeArray<TKey> _keys;
            [NativeDisableParallelForRestriction]
            private NativeArray<int> _indices;
            [NativeDisableParallelForRestriction]
            private NativeArray<TValue> _values;
        
            private readonly UnsafeExtensions.SharedIndex _addCurrentIndex;

            public KeyToValueIndexMapper(Allocator allocator = Allocator.TempJob,
                int defaultCapacity = 200)

            {
                _keys = new NativeArray<TKey>(defaultCapacity, allocator);
                _indices = new NativeArray<int>(defaultCapacity, allocator);
                _values = new NativeArray<TValue>(defaultCapacity, allocator);

                _addCurrentIndex = new UnsafeExtensions.SharedIndex(allocator);
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                value = default;
                return true;
            }

            public bool TryAdd(TKey key, TValue value)
            {
                return true;
            }
        
            public void Dispose()
            {
                _keys.Dispose();
                _indices.Dispose();
                _values.Dispose();
            
                _addCurrentIndex.Dispose();
            }
        }

        public static ref T GetRef<T>(this NativeArray<T> array, int index)
            where T : struct
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            unsafe
            {
                return ref UnsafeUtility.ArrayElementAsRef<T>(array.GetUnsafePtr(), index);
            }
        }

        public static ref U GetRef<TK, U>(this NativeHashMap<TK, U> map, TK key, Allocator allocator)
            where TK : struct, IEquatable<TK>
            where U : struct
        {
            var keyValueArrays = map.GetKeyValueArrays(allocator);
            var indexOfKey = keyValueArrays.Keys.IndexOf(key);
            return ref keyValueArrays.Values.GetRef(indexOfKey);
        }
    }
}