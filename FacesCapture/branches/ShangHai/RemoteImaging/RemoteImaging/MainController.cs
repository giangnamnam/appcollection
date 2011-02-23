using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.Imaging.Processors;
using Damany.PC.Domain;
using Damany.PortraitCapturer.DAL;
using RemoteImaging.Core;
using Damany.RemoteImaging.Common;
using RemoteImaging.RealtimeDisplay;
using Frame = Damany.Imaging.Common.Frame;
using Portrait = Damany.Imaging.Common.Portrait;

namespace RemoteImaging
{
    public class MainController
    {
        private readonly List<FaceSearchFacade> _faceSearchFacadeList = new List<FaceSearchFacade>();
        private readonly MainForm _mainForm;
        private readonly ConfigurationManager _configManager;
        private readonly Func<FaceSearchFacade> _faceSearchFacadeFactory;

        public MainController(RealtimeDisplay.MainForm mainForm,
                              ConfigurationManager configManager,
                              Func<FaceSearchFacade> faceSearchFacadeFactory)
        {
            this._mainForm = mainForm;
            _mainForm = mainForm;
            this._configManager = configManager;
            _configManager = configManager;
            _faceSearchFacadeFactory = faceSearchFacadeFactory;
        }

        public void Start()
        {
            this._mainForm.Cameras = this._configManager.GetCameras().ToArray();
            var camToStart = this._configManager.GetCameras();

            this.StartCameras();

        }

        public void Stop()
        {
            StopSearchFaceFacade();
        }

        private void StopSearchFaceFacade()
        {
            foreach (var faceSearchFacade in _faceSearchFacadeList)
            {
                faceSearchFacade.SignalToStop();
            }

            _faceSearchFacadeList.Clear();
        }

        public void SetupRoi()
        {
            if (_faceSearchFacadeList.Count > 0)
            {
                _faceSearchFacadeList[0].SetupMonitorRegion();
            }
        }


        public void StartCameras()
        {

            this.StopSearchFaceFacade();

            foreach (var cameraInfo in _configManager.GetCameras().Take(1))
            {
                try
                {
                    var searchFacade = _faceSearchFacadeFactory();
                    searchFacade.StartWith(cameraInfo);

                    if (cameraInfo.Provider == CameraProvider.Sanyo)
                    {
                        this._mainForm.StartRecord(cameraInfo);
                    }

                    _faceSearchFacadeList.Add(searchFacade);
                }
                catch (System.Net.WebException)
                {
                    var msg = string.Format("无法连接 {0}", cameraInfo.Location.Host);
                    _mainForm.ShowMessage(msg);

                }
            }

        }


        public void SelectedPortraitChanged()
        {

        }

        public void PlayVideo()
        {
            var p = _mainForm.SelectedObject;
            if (p == null) return;

            VideoPlayer.PlayRelatedVideo(p.CapturedAt, p.CapturedFrom.Id);
        }


        private void StartCameraInternal(CameraInfo cam)
        {
            System.Threading.WaitCallback action = delegate
            {
                try
                {
                    if (cam.Provider == CameraProvider.Sanyo)
                    {
                        this._mainForm.StartRecord(cam);
                    }

                }
                catch (System.Net.WebException ex)
                {
                    var msg = string.Format("无法连接 {0}", cam.Location.Host);
                    _mainForm.ShowMessage(msg);
                }

            };
            System.Threading.ThreadPool.QueueUserWorkItem(action);
        }



    }
}
