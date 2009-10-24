using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing
{
    public class FaceSearchConfiguration
    {
        public int EnvironmentMode { get; set; }
        public float LeftRation { get; set; }
        public float RightRation { get; set; }
        public float TopRation { get; set; }
        public float BottomRation { get; set; }
        public int MinFaceWidth { get; set; }
        public int MaxFaceWidth { get; set; }
        public System.Drawing.Rectangle SearchRectangle { get; set; }
    }
}
