using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutofacContrib.Startable;
using Damany.Imaging.Common;
using Damany.PC.Domain;
using Autofac;
using Damany.Imaging.PlugIns;
using MiscUtil;
using Damany.RemoteImaging.Common;
using Damany.RemoteImaging.Common;
using RemoteImaging.RealtimeDisplay;
using Frame = Damany.Imaging.Common.Frame;
using Portrait = Damany.Imaging.Common.Portrait;

namespace RemoteImaging
{
    class StartUp
    {
        public event EventHandler StatusChanged;

        private const string MissingImageName = "ImgMissing.jpg";

        public void Start()
        {
            CheckImportantFiles();

            this.builder = new ContainerBuilder();

            this.InitConfigManager();
            this.RegisterTypes();

            this.Container.Resolve<IStarter>().Start();
        }

        public static string GetMissingImagePath()
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MissingImageName);
        }

        private void CheckImportantFiles()
        {
            var filesToCheck = new string[]
                                   {
                                       "cv100.dll",
                                       "cvaux100.dll",
                                       "cvcam100.dll",
                                       "cxcore100.dll",
                                       "cxts001.dll",
                                       "highgui100.dll",
                                       "libguide40.dll",
                                       "ml100.dll",
                                       //"avdecoder.dll",
                                       "bk_netclientsdk.dll",
                                       "bkpostproc.dll",
                                       "transtclient.dll",
                                       "trclient.dll",
                                   };

            var dllsQuery = from f in System.IO.Directory.GetFiles(@".\", "*.dll")
                            select System.IO.Path.GetFileName(f).ToUpper();

            var dlls = dllsQuery.ToArray();


            foreach (var f in filesToCheck)
            {
                if (!dlls.Contains(f.ToUpper()))
                {
                    throw new System.IO.FileNotFoundException(f + " is missing");
                }
            }
        }


        private void InitConfigManager()
        {
            var configManger = ConfigurationManager.GetDefault();

            this.builder.RegisterInstance(configManger)
                .As<ConfigurationManager>()
                .ExternallyOwned();
        }

        private void RegisterTypes()
        {
            builder.RegisterType<MessageBoxService>()
                .As<IMessageBoxService>()
                .SingleInstance();

            builder.RegisterType<FileSystemStorage>()
                .WithParameter("outputRoot", Properties.Settings.Default.OutputPath)
                .SingleInstance();

            this.builder.RegisterType<Query.PicQueryForm>()
                .As<IPicQueryScreen>();
            this.builder.RegisterType<PicQueryFormPresenter>()
                .As<IPicQueryPresenter>();

            this.builder.RegisterType<Query.VideoQueryForm>()
                .As<Query.IVideoQueryScreen>();
            this.builder.RegisterType<Query.VideoQueryPresenter>()
                .As<Query.IVideoQueryPresenter>();

            this.builder.RegisterType<Damany.RemoteImaging.Common.Forms.FaceCompare>();
            this.builder.RegisterType<Damany.RemoteImaging.Common.Presenters.FaceComparePresenter>();

            if (Properties.Settings.Default.DetectMotion)
            {
                builder.RegisterType<FaceProcessingWrapper.MotionDetector>()
                .As<IMotionDetector>();
            }
            else
            {
                builder.RegisterType<NullMotionDetector>()
                    .As<IMotionDetector>();
            }

            builder.RegisterType<Damany.Imaging.Processors.MotionDetector>().WithParameter("workMode", Properties.Settings.Default.WorkingMode);

            var par = (Damany.Imaging.ConfigurationHandlers.FaceSearchConfigSectionHandler)System.Configuration.ConfigurationManager.GetSection("FaceSearchParameters");
            builder.RegisterType<Damany.Imaging.Processors.PortraitFinder>()
                   .WithProperty("Configuration", par)
                   .PropertiesAutowired();

            this.builder.RegisterType<OptionsForm>().SingleInstance();
            this.builder.RegisterType<OptionsPresenter>();

            this.builder.RegisterType<MainController>();

            builder.RegisterType<EventAggregator>()
                   .As<IEventAggregator>().SingleInstance();

            builder.RegisterType<RealtimeDisplay.MainForm>()
                   .As<YunTai.INavigationScreen>()
                   .As<RealtimeDisplay.MainForm>()
                   .SingleInstance()
                   .PropertiesAutowired();

            builder.RegisterType<FullVideoScreen>();

            RegisterNavControlTypes();

            builder.RegisterType<SearchLineBuilder>();

            builder.RegisterType<FaceSearchFacade>().WithProperty("MotionQueueSize", Properties.Settings.Default.MaxFrameQueueLength);

            builder.RegisterModule(new Autofac.Configuration.ConfigurationSettingsReader());

            builder.RegisterType<OutDatedDataRemover>().As<IMyStartable>()
                .WithParameter("outputDirectory", Properties.Settings.Default.OutputPath)
                .WithProperty("ReservedDiskSpaceInGb", Properties.Settings.Default.ReservedDiskSpaceGb); ;
            builder.RegisterType<VideoFileMonitor>().As<IMyStartable>().WithParameter("directoryToMonitor",
                                                                                      Util.GetVideoOutputPath());
            builder.RegisterType<VideoFileWalker>().As<IMyStartable>().WithParameter("directoryToWalk",
                                                                                     Util.GetVideoOutputPath());
            builder.RegisterModule(new StartableModule<IMyStartable>(s => s.Start()));
            //builder.RegisterModule(new LicensePlate.LicensePlateModule());)))));


            this.Container = this.builder.Build();

        }

        private void RegisterNavControlTypes()
        {
            builder.RegisterType<YunTai.NavigationController>();
        }


        public string Status { get; set; }
        public string OutputImagePath { get; set; }
        public IContainer Container { get; private set; }

        private ContainerBuilder builder;

    }
}
