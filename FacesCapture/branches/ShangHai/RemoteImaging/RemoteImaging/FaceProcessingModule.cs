using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using SuspectsRepository;

namespace RemoteImaging
{
    public class FaceProcessingModule : Autofac.Module
    {
        public bool EnableBackgroundComparer { get; set; }

        public bool EnableFrontFaceComparer { get; set; }
        public string FaceTemplatePath { get; set; }

        public bool EnableFaceComparer { get; set; }
        public string PersonOfInterestLibraryPath { get; set; }

        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<FaceComparer>()
                    .As<FaceComparer>()
                    .WithProperty("FaceAlignmentModelPath", GetAbsolutePath(Properties.Settings.Default.FaceAlignModelFile))
                    .WithProperty("FaceAlignClassifierPath", GetAbsolutePath(Properties.Settings.Default.FaceAlignClassifierFile))
                    .WithProperty("FaceRepositoryPath", GetAbsolutePath(Properties.Settings.Default.FaceRepositoryPath))
                    .WithProperty("IlluminateReferenceFilePath", GetAbsolutePath(Properties.Settings.Default.IlluminateRefFilePath))
                    .PropertiesAutowired();



            if (EnableFaceComparer)
            {
                if (System.IO.Directory.Exists(PersonOfInterestLibraryPath))
                {
                    var personRepository = SuspectsRepositoryManager.LoadFrom(PersonOfInterestLibraryPath);
                    builder.RegisterInstance(personRepository.Peoples)
                        .As<IEnumerable<PersonOfInterest>>()
                        .ExternallyOwned();
                }

                builder.RegisterType<LbpFaceComparer>()
                       .As<IRepositoryFaceComparer>();
            }
            else
            {
                builder.Register(c => new PersonOfInterest[0]).SingleInstance();
                builder.RegisterType<NullRepositoryFaceComparer>()
                       .As<IRepositoryFaceComparer>();
            }


            if (EnableFrontFaceComparer)
            {
                builder.RegisterType<Damany.Imaging.Handlers.FrontFaceVerifier>()
                    .As<IFaceSatisfyCompareCretia>()
                    .WithParameter("template", FaceTemplatePath);
            }



            if (EnableBackgroundComparer)
            {
                builder.RegisterType<Damany.Imaging.Handlers.FaceVerifier>()
                .As<IFacePostFilter>();
            }

        }

        private string GetAbsolutePath(string relativePath)
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }
    }
}
