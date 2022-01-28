using System;
using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Core.Extensions;
using Unity.Collections;

namespace Source.Scripts.Core
{
    public struct NativeArray2D<T> : IDisposable, IEnumerable<T> where T : struct
    {
        [NativeDisableParallelForRestriction] 
        public NativeArray<T> InnerArray;
        
        public readonly int Width;
        public readonly int Height;
            
        public NativeArray2D(int height, int width, Allocator allocator = Allocator.Temp, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
        {
            InnerArray = new NativeArray<T>(width * height, allocator, options);
            Width = width;
            Height = height;
        }
        
        public NativeArray2D(int height, int width, NativeArray<T> array)
        {
            InnerArray = array;
            Width = width;
            Height = height;
        }
        
        public ref T this[int row, int column] 
            => ref InnerArray.GetRef(row * Width + column);

        public void Dispose()
        {
            InnerArray.Dispose();
        }

        public IEnumerator<T> GetEnumerator() 
            => InnerArray.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}