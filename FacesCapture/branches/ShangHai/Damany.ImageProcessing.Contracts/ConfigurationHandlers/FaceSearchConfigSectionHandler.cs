using System.Configuration;

namespace Damany.Imaging.ConfigurationHandlers
{
    public class FaceSearchConfigSectionHandler : ConfigurationSection
    {
        private const string MinFaceWidthName = "MinFaceWidth";
        private const string MaxFaceWidthName = "MaxFaceWidth";

        [ConfigurationProperty(MinFaceWidthName, DefaultValue = 50)]
        public int MinFaceWidth
        {
            get { return (int)this[MinFaceWidthName]; }
            set { this[MinFaceWidthName] = value; }
        }

        [ConfigurationProperty(MaxFaceWidthName, DefaultValue = 300)]
        public int MaxFaceWidth
        {
            get { return (int)this[MaxFaceWidthName]; }
            set { this[MinFaceWidthName] = value; }
        }
    }
}
