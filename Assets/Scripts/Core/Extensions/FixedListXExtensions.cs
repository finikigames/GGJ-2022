using System;
using System.Collections.Generic;
using Unity.Collections;

namespace Source.Scripts.Core.Extensions
{
    public static class FixedListXExtensions
    {
        public static FixedList128Bytes2D<Coords> ToRect(this FixedList128Bytes<Coords> arr)
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

            var height = max.Row - min.Row + 1;
            var width = max.Column - min.Column + 1;
            
            var rect = new FixedList128Bytes2D<Coords>((sbyte)height, (sbyte)width);
            Fill(ref rect, Coords.MIN);

            foreach (var coords in arr)
            {
                rect[coords.Row - min.Row, coords.Column - min.Column] = coords;
            }

            return rect;
        }

        public static void Fill<T>(ref FixedList512Bytes2D<T> arr, T value) where T : unmanaged 
        {
            for (var i = 0; i < arr.Length(); i++)
            {
                arr.InnerList[i] = value;
            }
        }

        public static void Fill<T>(ref FixedList128Bytes2D<T> arr, T value) where T : unmanaged 
        {
            for (var i = 0; i < arr.Length() - 1; i++)
            {
                arr.InnerList[i] = value;
            }
        }

        public static int Length<T>(this FixedList128Bytes2D<T> array) where T : unmanaged
            => array.Height * array.Width;

        public static int Length<T>(this FixedList512Bytes2D<T> array) where T : unmanaged
            => array.Height * array.Width;


        public static bool In<T>(this FixedList128Bytes2D<T> arr, int row, int column) where T : unmanaged
        {
            if (row < 0 || column < 0) return false;
            if (row >= arr.Height) return false;
            if (column >= arr.Width) return false;

            return true;
        }

        public static bool In<T>(this FixedList512Bytes2D<T> arr, int row, int column) where T : unmanaged
        {
            if (row < 0 || column < 0) return false;
            if (row >= arr.Height) return false;
            if (column >= arr.Width) return false;

            return true;
        }

        // Generic context methods
        public static FixedListXBytes2D<Coords, TContainer> ToRect<TContainer>(this TContainer arr)
            where TContainer : struct, INativeList<Coords> , IEnumerable<Coords>
        {
            var min = Coords.MAX;
            var max = Coords.MIN;

            foreach (Coords coords in arr)
            {
                if (coords.Row < min.Row) min.Row = coords.Row;
                if (coords.Row > max.Row) max.Row = coords.Row;

                if (coords.Column < min.Column) min.Column = coords.Column;
                if (coords.Column > max.Column) max.Column = coords.Column;
            }

            var height = max.Row - min.Row + 1;
            var width = max.Column - min.Column + 1;
            
            var array = new TContainer {Length = height * width};
            var rect = new FixedListXBytes2D<Coords, TContainer>(array,
                (sbyte)height,
                (sbyte)width);
            Fill(ref rect, Coords.MIN);

            foreach (var coords in arr)
            {
                rect[coords.Row - min.Row, coords.Column - min.Column] = coords;
            }

            return rect;
        }

        public static void Fill<TType, TContainer>(ref FixedListXBytes2D<TType, TContainer> arr, TType value) 
            where TType : unmanaged 
            where TContainer : struct, INativeList<TType>, IEnumerable<TType>
        {
            for (var i = 0; i < arr.Length; i++)
            {
                arr.InnerList[i] = value;
            }
        }
        
        public static bool In<TType, TContainer>(this FixedListXBytes2D<TType, TContainer> arr, int row, int column) 
            where TType : unmanaged
            where TContainer : struct, INativeList<TType>, IEnumerable<TType>
        {
            if (row < 0 || column < 0) return false;
            if (row >= arr.Height) return false;
            if (column >= arr.Width) return false;

            return true;
        }
    }
}