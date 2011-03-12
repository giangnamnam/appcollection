using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    [Serializable]
    public class CapturedObject
    {
        public CapturedObject()
        {
            this.CapturedAt = DateTime.Now;
            this.Guid = System.Guid.NewGuid();
        }

        public DateTime CapturedAt { get; set; }
        public int DeviceId { get; set; }
        public System.Guid Guid { get; set; }
        public int Oid { get; set; }

    }
}
