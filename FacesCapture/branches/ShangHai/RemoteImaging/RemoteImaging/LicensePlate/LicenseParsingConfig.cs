using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RemoteImaging.LicensePlate
{
    public class LicenseParsingConfig : ConfigurationSection
    {
        private const string IncludeSubdirectoriesName = "IncludeSubdirectories";
        private const string LeastSectionsCountName = "LeastSectionCount";
        private const string LicensePlateSectionIndexName = "LicensePlateSectionIndex";
        private const string TimeSectionIndexName = "TimeSectionIndex";
        private const string SeparateCharName = "SeparateChar";
        private const string FilterName = "Filter";
        private const string ScanIntervalName = "ScanInterval";
        private const string LicensePlatePositionIndexName = "LicensePlatePositionSectionIndex";
        private const string LicensePlateWidthName = "LicensePlateWidth";
        private const string LicensePlateHeightName = "LicensePlateHeight";
        private const string LicensePlateCoordinateSeparateCharName = "LicensePlateCoordinateSeparateChar";
        private const string PlateColorSectionIndexName = "PlateColorSectionIndex";

        [ConfigurationProperty(IncludeSubdirectoriesName, DefaultValue = true)]
        public bool IncludeSubdirectories
        {
            get
            {
                return (bool)this[IncludeSubdirectoriesName];
            }
            set
            {
                this[IncludeSubdirectoriesName] = value;

            }
        }


        [ConfigurationProperty(LeastSectionsCountName, DefaultValue = 4)]
        public int LeastSectionCount
        {
            get
            {
                return (int)this[LeastSectionsCountName];
            }
            set
            {
                this[LeastSectionsCountName] = value;
            }
        }


        [ConfigurationProperty(TimeSectionIndexName, DefaultValue = 0)]
        public int TimeSectionIndex
        {
            get
            {
                return (int)this[TimeSectionIndexName];
            }
            set
            {
                this[TimeSectionIndexName] = value;
            }
        }



        [ConfigurationProperty(LicensePlateSectionIndexName, DefaultValue = 1)]
        public int LicensePlateSectionIndex
        {
            get
            {
                return (int)this[LicensePlateSectionIndexName];
            }
            set
            {
                this[LicensePlateSectionIndexName] = value;
            }
        }


        [ConfigurationProperty(LicensePlatePositionIndexName, DefaultValue = 2)]
        public int LicensePlatePositionIndex
        {
            get
            {
                return (int)this[LicensePlatePositionIndexName];
            }
            set
            {
                this[LicensePlatePositionIndexName] = value;
            }
        }

        [ConfigurationProperty(PlateColorSectionIndexName, DefaultValue = 3)]
        public int PlateColorIndex
        {
            get
            {
                return (int)this[PlateColorSectionIndexName];
            }
            set
            {
                this[PlateColorSectionIndexName] = value;
            }
        }

        [ConfigurationProperty(SeparateCharName, DefaultValue = '-')]
        public char SeparateChar
        {
            get
            {
                return (char)this[SeparateCharName];
            }
            set
            {
                this[SeparateCharName] = value;
            }
        }

        [ConfigurationProperty(FilterName, DefaultValue = "*.jpg")]
        public string Filter
        {
            get
            {
                return (string)this[FilterName];
            }
            set
            {
                this[FilterName] = value;
            }
        }

        [ConfigurationProperty(ScanIntervalName, DefaultValue = 30)]
        public int ScanInterval
        {
            get
            {
                return (int)this[ScanIntervalName];
            }
            set
            {
                this[ScanIntervalName] = value;
            }
        }



        [ConfigurationProperty(LicensePlateWidthName, DefaultValue = 150)]
        public int LicensePlateWidth
        {
            get
            {
                return (int)this[LicensePlateWidthName];
            }
            set
            {
                this[LicensePlateWidthName] = value;
            }
        }

        [ConfigurationProperty(LicensePlateHeightName, DefaultValue = 50)]
        public int LicensePlateHeight
        {
            get
            {
                return (int)this[LicensePlateHeightName];
            }
            set
            {
                this[LicensePlateHeightName] = value;
            }
        }

        [ConfigurationProperty(LicensePlateCoordinateSeparateCharName, DefaultValue = ',')]
        public char LicensePlateCoordinateSeparator
        {
            get
            {
                return (char)this[LicensePlateCoordinateSeparateCharName];
            }
            set
            {
                this[LicensePlateCoordinateSeparateCharName] = value;
            }
        }


    }
}
