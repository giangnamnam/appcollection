using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSZN.Component;

namespace RemoteImaging
{
    class MockCamera : ICamera
    {
        int idx;
        string[] files;

        public MockCamera(string path)
        {
            files = System.IO.Directory.GetFiles(path);

        }

        #region ICamera Members

        public System.Drawing.Image CaptureImage()
        {
            throw new NotImplementedException();
        }

        public byte[] CaptureImageBytes()
        {
            if (idx == files.Length - 1)
            {
                idx = 0;
            }
            string file = files[idx++];

            System.Diagnostics.Debug.WriteLine(idx);


            return System.IO.File.ReadAllBytes(file);
        }

        #endregion
    }
}
