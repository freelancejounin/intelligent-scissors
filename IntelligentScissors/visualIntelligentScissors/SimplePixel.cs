using System;
using System.Collections.Generic;
using System.Drawing;

namespace VisualIntelligentScissors
{
    class SimplePixel
    {
        private int _x;
        private int _y;
        private Boolean _visited;

        public SimplePixel(int x, int y)
        {
            _x = x;
            _y = y;
            _visited = false;
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
            get { return _visited; }
            set { _visited = value; }
        }
    }
}
