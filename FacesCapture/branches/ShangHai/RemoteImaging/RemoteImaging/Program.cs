﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.ServiceModel;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using RemoteControlService;
using Autofac;
using RemoteImaging.ConfigurationSectionHandlers;
using RemoteImaging.LicensePlate;


namespace RemoteImaging
{
    using RealtimeDisplay;
    using System.Xml;

    static class Program
    {
        public static string directory;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += Application_ThreadException;


#if !DEBUG
            //if (!Util.VerifyKey())
            //{
            //    using (var form = new RegisterForm())
            //    {
            //        DialogResult res = form.ShowDialog();
            //        if (res == DialogResult.OK)
            //            Application.Restart();
            //    }

            //    return;
            //}
#endif
            try
            {
                DevExpress.UserSkins.OfficeSkins.Register();
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();

                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Devexpress Style");


                int count = 0;
                IDataStore dataStore = null;
            RETRY:
                try
                {
                    dataStore = XpoDefault.GetConnectionProvider(
                        MSSqlConnectionProvider.GetConnectionString(Properties.Settings.Default.SqlInstanceName, "FaceCapture"),
                        AutoCreateOption.DatabaseAndSchema);

                }
                catch (Exception ex)
                {
                    ++count;
                    if (count == 3)
                    {
                        MessageBox.Show("无法连接数据库，请确认数据库已经正确安装。", "数据库", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        //让sqlserver有时间启动。
                        System.Threading.Thread.Sleep(3000);
                        goto RETRY;
                    }
                }


                using (SimpleDataLayer dataLayer = new SimpleDataLayer(dataStore))
                {
                    using (Session session = new Session(dataLayer))
                    {
                        session.UpdateSchema();
                        session.CreateObjectTypeRecords(
                            typeof(Damany.PortraitCapturer.DAL.DTO.CapturedImageObject),
                            typeof(Damany.PortraitCapturer.DAL.DTO.Video),
                            typeof(Damany.PortraitCapturer.DAL.DTO.Portrait),
                            typeof(Damany.PortraitCapturer.DAL.DTO.Frame),
                            typeof(Damany.PortraitCapturer.DAL.DTO.TargetPerson));
                        var cachNode = new DataCacheNode(new DataCacheRoot(dataStore));
                        XpoDefault.DataLayer = new ThreadSafeDataLayer(session.Dictionary, cachNode);
                    }
                }

                try
                {

#if !DEBUG
                    //if (!Util.VerifyKey())
                    //{
                    //    RegisterForm form = new RegisterForm();
                    //    DialogResult res = form.ShowDialog();
                    //    if (res == DialogResult.OK)
                    //    {
                    //        Application.Restart();
                    //    }

                    //    return;
                    //}
#endif

                    var modeCollection = new DevExpress.Xpo.XPCollection<WorkModeCamSetting>();
                    modeCollection.Load();

                    if (modeCollection.Count == 0)
                    {
                        var workingModeSettings = new WorkModeCamSetting();
                        workingModeSettings.ModeName = "室外模式";
                        workingModeSettings.ShutterSpeed = 3;
                        workingModeSettings.IrisLevel = 40;
                        workingModeSettings.Save();

                        workingModeSettings = new WorkModeCamSetting();
                        workingModeSettings.ShutterSpeed = 1;
                        workingModeSettings.IrisLevel = 50;
                        workingModeSettings.ModeName = "室内模式";
                        workingModeSettings.Save();
                    }

                    foreach (var workModeCamSetting in modeCollection)
                    {
                        var dummy = workModeCamSetting;
                        System.Diagnostics.Debug.WriteLine(dummy);
                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var strapper = new StartUp();
                    strapper.Start();

                    var mainForm = strapper.Container.Resolve<RemoteImaging.RealtimeDisplay.MainForm>();
                    var controller = strapper.Container.Resolve<MainController>();
                    mainForm.AttachController(controller);

                    mainForm.ButtonsVisible =
                        (ButtonsVisibleSectionHandler)System.Configuration.ConfigurationManager.GetSection("FaceDetector.ButtonsVisible");

                    try
                    {
                        //StartLicensePlateMonitor(strapper.Container);
                        WireupNavigation(strapper.Container);

                        //RegisterLicensePlateRepository(strapper);
                    }
                    catch (Exception e)
                    {
                        HandleException(e);
                    }

                    Application.Run(mainForm);

                }
                catch (Exception e)
                {
                    HandleException(e);
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }

        private static void RegisterLicensePlateRepository(StartUp strapper)
        {
            var repository = strapper.Container.Resolve<LicensePlateRepository>();
            var suspectLicensePlateCheck = strapper.Container.Resolve<SuspectLicensePlateChecker>();
        }


        private static void WireupNavigation(Autofac.IContainer container)
        {
            var navController = container.Resolve<YunTai.NavigationController>();
            navController.Start();
        }

        private static void StartLicensePlateMonitor(Autofac.IContainer container)
        {
            var factory = container.Resolve<LicensePlate.LicensePlateUploadMonitor.Factory>();

            var manager = container.Resolve<Damany.RemoteImaging.Common.ConfigurationManager>();
            foreach (var cam in manager.GetCameras())
            {
                if (cam.LicensePlateUploadDirectory != null)
                {
                    if (!System.IO.Directory.Exists(cam.LicensePlateUploadDirectory))
                    {
                        throw new System.IO.DirectoryNotFoundException(@"车牌上传目录 """ + cam.LicensePlateUploadDirectory + "\"不存在，请重新配置系统！");
                    }

                    var m = factory.Invoke(cam.LicensePlateUploadDirectory);
                    m.CameraId = cam.Id;
                    m.Configuration = (LicenseParsingConfig)System.Configuration.ConfigurationManager.GetSection("LicenseParsingConfig");
                    m.Start();
                    GC.KeepAlive(m);
                }
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void HandleException(Exception e)
        {
            LogException(e);
            ShowException(e);
        }

        private static void ShowException(System.Exception e)
        {
            MessageBox.Show(e.Message, "发生异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void LogException(System.Exception e)
        {
            Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(
                e, Constants.ExceptionHandlingLogging
                );
        }


        static void watcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            DateTime time = args.Images[0].CaptureTime;
            string msg = string.Format("camID={0} count={1} time={2}", args.CameraID, args.Images.Length, time);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
