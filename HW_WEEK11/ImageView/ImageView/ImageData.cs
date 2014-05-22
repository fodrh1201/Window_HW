using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageView
{
    public class ImageData
    {
        public int width;
        public int height;
        public int[] pixel;
        public ImageData()
        {
        }

        public void SetSize(int w, int h)
        {
            width = w;
            height = h;
            pixel = new int[w * h];
        }
    }
}
