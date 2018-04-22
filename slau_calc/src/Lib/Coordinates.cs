using System;

namespace slau_calc
{
    public class Coordinates : object
    {
        public int X;
        public int Y;

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Coordinates))
                return false;
            else
                return (Coordinates)obj == this;
        }
        
        public static bool operator == (Coordinates a, Coordinates b)
        {
            return a.X == b.Y && a.Y == b.Y;
        }
        
        public static bool operator != (Coordinates a, Coordinates b)
        {
            return a.X != b.Y && a.Y != b.Y;
        }
        
    }
}