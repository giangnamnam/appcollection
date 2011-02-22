using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Damany.Imaging.Common;
using DevExpress.Xpo;
using Microsoft.Win32;
using System.Windows.Forms;
using RemoteImaging.Core;

namespace RemoteImaging
{
    public static class VideoPlayer
    {
        private static string videoPlayerPath;

        static VideoPlayer()
        {
            try
            {
                videoPlayerPath = (string)Registry.LocalMachine.OpenSubKey("Software")
                                .OpenSubKey("Videolan")
                                .OpenSubKey("vlc").GetValue(null);
            }
            catch (Exception)
            {
                videoPlayerPath = null;
            }

        }


        public static string ExePath { get { return videoPlayerPath; } }

        public static void PlayRelatedVideo(DateTime captureTime, int cameraId)
        {
            var imgInfo = new ImageDetail();
            imgInfo.CaptureTime = captureTime;
            imgInfo.FromCamera = cameraId;

            using (var session = new Session())
            {
                var c = DevExpress.Data.Filtering.CriteriaOperator.Parse("CaptureTime = ?",
                                                                         captureTime.Date.AddHours(captureTime.Hour).
                                                                             AddMinutes(captureTime.Minute));
                var v = (Damany.PortraitCapturer.DAL.DTO.Video)session.FindObject(typeof(Damany.PortraitCapturer.DAL.DTO.Video), c);

                if (v == null || !File.Exists(v.Path))
                {
                    MessageBox.Show("没有找到相关视频", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                PlayVideosAsync(new string[] { v.Path });
            }
        }


        public static void PlayVideosAsync(string[] videos)
        {
            if (videoPlayerPath == null)
            {
                MessageBox.Show("请安装VLC播放器!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            StringBuilder sb = new StringBuilder();
            foreach (var file in videos)
            {
                sb.Append(file); sb.Append(' ');
            }

            sb.Append(@"vlc://quit"); sb.Append(' ');

            Process.Start(videoPlayerPath, sb.ToString());
        }

    }
}
