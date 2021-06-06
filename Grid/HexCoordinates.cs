using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ArcaneRecursion
{
    [System.Serializable]
    public struct HexCoordinates
    {
        public int X { get { return this._x; } }
        public int Z { get { return this._z; } }
        public int Y { get { return -X - Z; } }

        private readonly int _x;
        private readonly int _z;

        #region  Init
        public HexCoordinates(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public HexCoordinates(HexCoordinates from, HexCoordinates to)
        {
            int distance = from.DistanceTo(to);
            if (distance == 0)
            {
                _x = 0;
                _z = 0;
            }
            else
            {
                _x = (int)Math.Round((float)(to.X - from.X) / (float)distance);
                _z = (int)Math.Round((float)(to.Z - from.Z) / (float)distance);
            }
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - (z / 2), z);
        }
        #endregion /* Init */

        public static BasicOrientation GetOrientation(HexCoordinates from, HexCoordinates to)
        {
            if (from.IsEqual(to))
                return BasicOrientation.Back;
            else if (from.IsOpposed(to))
                return BasicOrientation.Front;

            HexCoordinates fromToOffset = new HexCoordinates(to.X - from.X, to.Z - from.Z);
            if (Math.Abs(fromToOffset.X) + Math.Abs(fromToOffset.Z) > 1)
                return BasicOrientation.FrontSide;
            return BasicOrientation.BackSide;
        }

        #region Rotation
        public static HexCoordinates RotateClockwise60(HexCoordinates from)
        {
            return new HexCoordinates(-from.Y, -from.X);
        }

        public static HexCoordinates RotateClockwise120(HexCoordinates from)
        {
            return new HexCoordinates(from.Z, from.Y);
        }

        public static HexCoordinates Rotate180(HexCoordinates from)
        {
            return new HexCoordinates(-from.X, -from.Z);
        }

        public static HexCoordinates RotateCounterClockwise60(HexCoordinates from)
        {
            return new HexCoordinates(-from.Z, -from.Y);
        }

        public static HexCoordinates RotateCounterClockwise120(HexCoordinates from)
        {
            return new HexCoordinates(from.Y, from.X);
        }
        #endregion /* Rotation */

        public bool IsEqual(HexCoordinates other)
        {
            return X == other.X && Z == other.Z;
        }

        public bool IsOpposed(HexCoordinates other)
        {
            return -X == other.X && -Z == other.Z;
        }

        public int DistanceTo(HexCoordinates other)
        {
            return
                ((X < other.X ? other.X - X : X - other.X)
                + (Y < other.Y ? other.Y - Y : Y - other.Y)
                + (Z < other.Z ? other.Z - Z : Z - other.Z)) / 2;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
        }
    }
}