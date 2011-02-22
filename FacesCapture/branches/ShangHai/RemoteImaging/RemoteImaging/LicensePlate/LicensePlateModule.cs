using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LicensePlateCheckService>()
                .As<ILicensePlateCheckService>()
                .SingleInstance();

            builder.RegisterType<SuspectCarFormPresenter>()
                .As<INotifySuspeciousLicensePlate>();


            builder.RegisterType<TcpLicenseplateRecognizerController>()
                .As<ILicenseplateRecognizerController>()
                .WithParameter("peerIp", Properties.Settings.Default.LprIp)
                .WithParameter("port", Properties.Settings.Default.LprPort)
                .SingleInstance();

            builder.RegisterType<SuspectLicensePlateChecker>().SingleInstance();

            builder.RegisterType<FormSuspectCarManager>();

            builder.RegisterType<FormSuspectCarQuery>();

            LicensePlate.LicensePlateInfo.SubstituteImagePath = StartUp.GetMissingImagePath();

            builder.RegisterType<LicensePlate.LicensePlateEventPublisher>()
                .As<LicensePlate.ILicensePlateEventPublisher>()
                .SingleInstance();

            builder.RegisterType<LicensePlate.LicensePlateUploadMonitor>();

            builder.RegisterType<LicensePlate.LicensePlateDataProvider>()
                .As<LicensePlate.ILicensePlateDataProvider>()
                .WithParameter("outputDirectory", Properties.Settings.Default.OutputPath)
                .SingleInstance();

            builder.RegisterType<LicensePlate.LicensePlateRepository>()
                .WithParameter("outputDirectory", Properties.Settings.Default.OutputPath)
                .PropertiesAutowired()
                .SingleInstance();

            builder.RegisterType<LicensePlate.FormLicensePlateQuery>()
                .As<LicensePlate.ILicenseplateSearchScreen>();

            builder.RegisterType<LicensePlate.LicensePlateSearchPresenter>()
                .As<LicensePlate.ILicensePlateSearchPresenter>();



        }
    }
}
