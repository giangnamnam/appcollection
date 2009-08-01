using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace JSZN.Component
{
    public interface ICamera
    {
        Image CaptureImage();
        byte[] CaptureImageBytes();
    }
}
