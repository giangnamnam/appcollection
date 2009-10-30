using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RemoteControlService;
using RemoteImaging.Query;
using System.Drawing;


namespace RemoteImaging.Service
{
    [ServiceKnownType(typeof(System.Drawing.Bitmap))]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    class Service : IServiceFacade
    {
        string[] FaceFiles;

        #region IServiceFacade Members

        public int BeginSearchFaces(int cameraID, DateTime beginTime, DateTime endTime)
        {
            ImageDirSys start =
                new ImageDirSys(cameraID.ToString("d2"), DateTimeInString.FromDateTime(beginTime));
            ImageDirSys end =
                 new ImageDirSys(cameraID.ToString("d2"), DateTimeInString.FromDateTime(endTime));

            FaceFiles = ImageSearch.SearchImages(start, end, ImageDirSys.SearchType.PicType);

            return FaceFiles.Length;
        }

        public ImagePair GetFace(int idx)
        {
            string path = FaceFiles[idx];

            Bitmap face = (Bitmap)Image.FromFile(path);
            face.Tag = path;

            Bitmap big = (Bitmap)Image.FromFile(@"L:\pictures in hall\02_090718174534-0000.jpg");
            big.Tag = @"L:\pictures in hall\02_090718174534-0000.jpg";


            ImagePair ip = new ImagePair();
            ip.Face = face;
            ip.BigImage = big;

            return ip;

        }

        public void EndSearchFaces()
        {

        }

        #endregion
    }
}
