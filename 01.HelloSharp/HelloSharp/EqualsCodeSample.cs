using System;
using System.Collections.Generic;

namespace HelloSharp
{
    internal struct Point2DStruct: IEquatable<Point2DStruct>
    {
        public bool Equals(Point2DStruct other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Point2DStruct && Equals((Point2DStruct)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Point2DStruct left, Point2DStruct right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point2DStruct left, Point2DStruct right)
        {
            return !(left == right);
        }

        public readonly int X;
        public readonly int Y;

        public Point2DStruct(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("Point2DStruct({0}, {1}", Convert.ToString(X), Convert.ToString(Y));
        }
    }

    internal class Point2DObject: IEquatable<Point2DObject>
    {
        public static bool operator ==(Point2DObject left, Point2DObject right)
        {
            if (ReferenceEquals(left, null))
                return (ReferenceEquals(right, null));
            return left.Equals(right);
        }

        public static bool operator !=(Point2DObject left, Point2DObject right)
        {
            return !(left == right);
        }

        public bool Equals(Point2DObject other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Point2DObject)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public int X { get; }
        public int Y { get; }

        public Point2DObject(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("Point2DObject({0}, {1}", Convert.ToString(X), Convert.ToString(Y));
        }

    }

    internal class EqualsSample
    {
        public static void Run()
        {
            var d1 = new Dictionary<Point2DStruct, int>();
            var d2 = new Dictionary<Point2DObject, int>();

            for (int i = 0; i < 10; ++i)
            {
                d1.Add(new Point2DStruct(i, i * 10), i);
                d2.Add(new Point2DObject(i, i * 10), i);
            }


            for (int i = 0; i < 15; ++i)
            {
                string s1;
                string s2;
                var k1 = new Point2DStruct(i, i * 10);
                var k2 = new Point2DObject(i, i * 10);
                if (d1.ContainsKey(k1))
                    s1 = string.Format("Key: {0}; Value {1}", k1.ToString(), Convert.ToString(d1[k1]));
                else
                    s1 = string.Format("Key: {0} not found", k1.ToString());

                if (d2.ContainsKey(k2))
                    s2 = string.Format("Key: {0}; Value {1}", k2.ToString(), Convert.ToString(d2[k2]));
                else
                    s2 = string.Format("Key: {0} not found", k2.ToString());

                Console.WriteLine(s1);
                Console.WriteLine(s2);
            }
        }
    }
}