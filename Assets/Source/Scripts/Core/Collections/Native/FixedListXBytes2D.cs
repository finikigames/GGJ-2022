using System;
using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Core.Collections.Native.Base;
using Unity.Collections;

namespace Source.Scripts.Core.Collections.Native
{
    public struct FixedList128Bytes2D<T> : IEnumerable<T> where T : unmanaged
    {
        public FixedList128Bytes<T> InnerList;

        public readonly sbyte Width;
        public readonly sbyte Height;

        public FixedList128Bytes2D(sbyte height, sbyte width)
        {
            InnerList = new FixedList128Bytes<T> {Length = height * width};
            Width = width;
            Height = height;
        }
        
        public FixedList128Bytes2D(FixedList128Bytes<T> innerList, sbyte height, sbyte width)
        {
            InnerList = innerList;
            Width = width;
            Height = height;
        }
        
        public ref T this[int row, int column] 
            => ref InnerList.ElementAt(row * Width + column);

        public IEnumerator<T> GetEnumerator() 
            => InnerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    
    public struct FixedList512Bytes2D<T> : IEnumerable<T> where T : unmanaged
    {
        public FixedList512Bytes<T> InnerList;

        public readonly sbyte Width;

        public readonly sbyte Height;

        public FixedList512Bytes2D(FixedList512Bytes<T> innerList, sbyte height, sbyte width)
        {
            InnerList = innerList;
            Width = width;
            Height = height;
        }

        public ref T this[int row, int column] 
            => ref InnerList.ElementAt(row * Width + column);
        
        public IEnumerator<T> GetEnumerator() 
            => InnerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }  
    
    public struct FixedListXBytes2D<TType, TContainer> : INativeList2D<TType>, 
                                                         IEnumerable<TType> 
        where TType : unmanaged
        where TContainer : struct, INativeList<TType>, 
                                   IEnumerable<TType>
    {
        public TContainer InnerList;

        public readonly sbyte Width;

        public readonly sbyte Height;

        public bool IsEmpty => InnerList.IsEmpty;

        public int Capacity
        {
            get => InnerList.Capacity;
            set { }
        }

        public int Length
        {
            get => InnerList.Length;
            set => InnerList.Length = value;
        }

        public FixedListXBytes2D(sbyte height, sbyte width)
        {
            InnerList = new TContainer
            {
                Length = height * width
            };
            Width = width;
            Height = height;
        }

        public FixedListXBytes2D(TContainer innerList, sbyte height, sbyte width)
        {
            InnerList = innerList;
            Width = width;
            Height = height;
        }

        public ref TType this[int row, int column] 
            => ref InnerList.ElementAt(row * Width + column);

        public TType this[int index]
        {
            get => InnerList[index];
            set => InnerList[index] = value;
        }

        public ref TType ElementAt(int index)
        {
            return ref InnerList.ElementAt(index);
        }

        public void Clear()
            => InnerList.Clear();

        public IEnumerator<TType> GetEnumerator() 
            => InnerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();
    }
}