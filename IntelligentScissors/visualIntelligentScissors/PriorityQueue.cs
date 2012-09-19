using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace VisualIntelligentScissors
{
    class PriorityQueue
    {
        private ArrayList _pixels;

        public PriorityQueue()
        {
            _pixels = new ArrayList();
        }

        public Boolean hasNextPixel()
        {
            if (_pixels.Count > 0)
                return true;

            return false;
        }

        public IntelligentPixel nextPixel
        {
            get
            {
                IntelligentPixel temp = null;
                int min = 999999;
                int tempInd = -1;

                for (int i = 0; i < _pixels.Count; i++)
                {
                    if (((IntelligentPixel)_pixels[i]).Priority < min)
                    {
                        temp = (IntelligentPixel)_pixels[i];
                        tempInd = i;
                        min = temp.Priority;
                    }
                }

                if (tempInd != -1)
                    _pixels.RemoveAt(tempInd);

                return temp;
            }
        }

        public void Add(IntelligentPixel newPixel)
        {
            _pixels.Add(newPixel);
        }

        public Boolean Contains(IntelligentPixel ip)
        {
            for (int i = 0; i < _pixels.Count; i++)
                if (((IntelligentPixel)_pixels[i]).Equals(ip))
                    return true;

            return false;
        }

    }
}
