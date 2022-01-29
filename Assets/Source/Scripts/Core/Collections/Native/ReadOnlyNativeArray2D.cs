using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

namespace Source.Scripts.Core.Collections.Native
{
    public struct ReadOnlyNativeArray2D<T> : IEnumerable<T> where T : struct
    {
        [ReadOnly] 
        [NativeDisableParallelForRestriction]
        public NativeArray<T>.ReadOnly InnerArray;

        public readonly int Width;
        public readonly int Height;

        public ReadOnlyNativeArray2D(NativeArray<T> array, int width, int height)
        {
            InnerArray = array.AsReadOnly();
            Width = width;
            Height = height;
        }
            
        public T this[int row, int column] 
            => InnerArray[row * Width + column];
            
        public IEnumerator<T> GetEnumerator()
            => InnerArray.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => InnerArray.GetEnumerator();
    }
}