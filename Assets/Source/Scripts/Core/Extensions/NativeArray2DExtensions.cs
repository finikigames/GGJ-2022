using System;
using Source.Scripts.Core.Collections.Native;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Source.Scripts.Core.Extensions
{
    public static class NativeArray2DExtensions
    {
        [BurstCompile]
        public static NativeArray<T> Flat<T>(this NativeArray2D<T> arr, Allocator allocator) where T : struct
        {
            var newArray = new NativeArray<T>(arr.Length(), allocator);
            NativeArray<T>.Copy(arr.InnerArray, newArray);

            return newArray;
        }
            
        public static NativeArray2D<Coords> ToRect(this NativeList<Coords> arr)
        {
            var min = Coords.MAX;
            var max = Coords.MIN;

            foreach (var coords in arr)
            {
                if (coords.Row < min.Row) min.Row = coords.Row;
                if (coords.Row > max.Row) max.Row = coords.Row;

                if (coords.Column < min.Column) min.Column = coords.Column;
                if (coords.Column > max.Column) max.Column = coords.Column;
            }

            var rect = new NativeArray2D<Coords>(max.Row - min.Row + 1, max.Column - min.Column + 1, options: NativeArrayOptions.UninitializedMemory);
            rect.Fill(Coords.MIN);

            foreach (var coords in arr)
            {
                rect[coords.Row - min.Row, coords.Column - min.Column] = coords;
            }

            return rect;
        }
        
        public static void Resize<T>(this NativeArray2D<T> array) where T : unmanaged
        {
            //var nativeArray = new NativeArray<>()
        }
        
        public static int Length<T>(this NativeArray2D<T> array) where T : struct
            => array.Height * array.Width; 
        
        public static void Fill<T>(this NativeArray2D<T> arr, T value) where T : struct 
        {
            for (var i = 0; i < arr.Length() - 1; i++)
            {
                arr.InnerArray[i] = value;
            }
        }

        public static NativeArray2D<T> Copy<T>(this NativeArray2D<T> array, Allocator allocator) where T : unmanaged
        {
            var innerArrayCopy = new NativeArray<T>(array.Length(), allocator);
            NativeArray<T>.Copy(array.InnerArray, innerArrayCopy);

            return new NativeArray2D<T>(array.Height, array.Width, innerArrayCopy);
        }
        
        
        public static int RealIndex<T>(this NativeArray2D<T> array, int row, int column)  where T : unmanaged
            => row * array.Width + column;
        
        public static ref T GetByCoords<T>(this NativeArray2D<T> arr, Coords coords) where T : unmanaged
        {
            if (!arr.In(coords.Row, coords.Column))
            {
                throw new ArgumentOutOfRangeException($"Try to get element by {coords}");
            }

            return ref arr.GetRef(coords);
        }

        public static int Rows<T>(this NativeArray2D<T> array) where T : struct => array.Height;
        
        public static int Columns<T>(this NativeArray2D<T> array) where T : struct => array.Width;

        public static bool In<T>(this NativeArray2D<T> arr, Coords coords) where T : struct
        {
            if (coords.Row < 0 || coords.Column < 0) return false;
            if (coords.Row >= arr.Height) return false;
            if (coords.Column >= arr.Width) return false;

            return true;
        }
        
        public static ref T GetRef<T>(this NativeArray2D<T> array, Coords coords)
            where T : unmanaged
        {
            if (!array.In(coords.Row, coords.Column))
                throw new ArgumentOutOfRangeException(nameof(coords.Row) + coords.Column);
            unsafe
            {
                return ref UnsafeUtility.ArrayElementAsRef<T>(array.InnerArray.GetUnsafePtr(), 
                    array.RealIndex(coords.Row, coords.Column));
            }
        }
        
        public static bool In<T>(this NativeArray2D<T> arr, int row, int column) where T : struct
        {
            if (row < 0 || column < 0) return false;
            if (row >= arr.Height) return false;
            if (column >= arr.Width) return false;

            return true;
        }
        
        public static bool In<T>(this NativeArray<T> arr, int index) where T : struct
        {
            if (index < 0) return false;
            if (index >= arr.Length) return false;

            return true;
        }
    }
}