﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Drawing;

namespace RemoteControlService
{
    [ServiceContract]
    [ServiceKnownType(typeof(Bitmap))]
    public interface IServiceFacade
    {
        [OperationContract]
        string[] SearchFaces(int cameraID, DateTime from, DateTime to);

        [OperationContract]
        ImagePair GetFace(string path);

        [OperationContract]
        Video[] SearchVideos(int cameraID, DateTime from, DateTime to);

        [OperationContract]
        Bitmap[] FacesCapturedAt(int cameraID, DateTime time);

        [OperationContract]
        void BroadcastVideo(string path);

        [OperationContract]
        string VideoFilePathRecordedAt(DateTime time, int camID);

        [OperationContract]
        void KillPlayer();
    }


    [DataContract]
    public class Video
    {
        [DataMember]
        public bool HasFaceCaptured { get; set; }

        [DataMember]
        public string Path { get; set; }
    }


    [DataContract]
    public class ImagePair
    {
        [DataMember]
        public Bitmap Face { get; set; }

        [DataMember]
        public string FacePath { get; set; }

        [DataMember]
        public Bitmap BigImage { get; set; }

        [DataMember]
        public string BigImagePath { get; set; }
    }
}
