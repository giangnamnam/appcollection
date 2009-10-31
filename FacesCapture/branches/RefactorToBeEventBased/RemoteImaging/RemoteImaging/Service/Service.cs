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

            string bigImgPath = FileSystemStorage.BigImgPathForFace(Core.ImageDetail.FromPath(path));

            Bitmap big = (Bitmap)Image.FromFile(bigImgPath);

            ImagePair ip = new ImagePair();
            ip.Face = face;
            ip.FacePath = path;

            ip.BigImage = big;
            ip.BigImagePath = bigImgPath;

            return ip;
        }

        public void EndSearchFaces()
        {

        }

        #endregion
    }
}
