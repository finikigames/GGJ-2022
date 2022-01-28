using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = System.Object;

namespace Source.Scripts.Core
{
    [Serializable]
    public struct Coords : IEquatable<Coords>
    {
        public sbyte Row { get; set; }
        public sbyte Column { get; set; }

        public Coords(sbyte row, sbyte column)
        {
            this.Row = row;
            this.Column = column;
        }

        public bool IsDifferentValues()
        {
            return Row != Column;
        }

        public bool IsSomeValueEqual(sbyte value)
        {
            return Row == value || Column == value;
        }

        public override int GetHashCode()
        {
            return Column.GetHashCode() + Row.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            return obj is Coords coords && this == coords;
        }

        public static bool operator ==(Coords x, Coords y)
        {
            return x.Column == y.Column && x.Row == y.Row;
        }

        public static bool operator !=(Coords x, Coords y)
        {
            return !(x == y);
        }

        public static Coords operator +(Coords x, Coords y)
        {
            return new Coords((sbyte)(x.Row + y.Row), (sbyte)(x.Column + y.Column));
        }

        public static Coords operator -(Coords x, Coords y)
        {
            return new Coords((sbyte)(x.Row - y.Row), (sbyte)(x.Column - y.Column));
        }

        public override string ToString()
        {
            return $"row: {Row}, column: {Column}";
        }

        public bool InIntervalZeroTo(int row, int column)
        {
            if (this.Row < 0 || this.Column < 0) return false;
            return this.Row < row && this.Column < column;
        }

        public bool InIntervalZeroTo(Coords value)
        {
            return InIntervalZeroTo(value.Row, value.Column);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(Column, Row);
        }

        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(Column, Row);
        }

        public Coords Copy()
        {
            return new Coords(Row, Column);
        }

        public void Insert(Coords value)
        {
            Row = value.Row;
            Column = value.Column;
        }

        public Coords DeltaAbs(Coords other)
        {
            return new Coords((sbyte)Math.Abs(Row - other.Row), (sbyte)Math.Abs(Column - other.Column));
        }

        public bool Equals(Coords other)
        {
            return other.Column == Column && other.Row == Row;
        }

        public static Coords MIN => new(sbyte.MinValue, sbyte.MinValue);

        public static Coords MAX => new(sbyte.MaxValue, sbyte.MaxValue);
    }
}