﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.PortraitCapturer.DAL
{
    public class MockFrameSource : Damany.Imaging.Common.IFrameStream
    {
        #region IFrameStream Members

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public Damany.Imaging.Common.Frame RetrieveFrame()
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get;
            set;
        }

        public string Description
        {
            get { return "Database"; }
        }

        #endregion
    }
}
