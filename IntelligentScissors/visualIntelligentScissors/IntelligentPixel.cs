using System;
using System.Collections.Generic;
using System.Drawing;

namespace VisualIntelligentScissors
{
    public class IntelligentPixel : IComparable<IntelligentPixel>
    {
        private int _x;
        private int _y;
        private Boolean visited;
        private int cost;
        private IntelligentPixel prev;

        public IntelligentPixel(int x, int y)
        {
            _x = x;
            _y = y;
            visited = false;
            cost = -1;
            prev = null;
        }

        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
        }

        public Boolean Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        public int Priority
        {
            get { return cost; }
            set { cost = value; }
        }

        public IntelligentPixel Prev
        {
            get { return prev; }
            set { prev = value; }
        }

        public int CompareTo(IntelligentPixel b)
        {
            return this.Priority.CompareTo(b.Priority);
        }

        public Boolean Equals(IntelligentPixel a)
        {
            if ((this.X == a.X) && (this.Y == a.Y))
                return true;

            return false;
        }
    }
}
