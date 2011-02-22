using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class SuspectCarAlermHandleInfo
    {
        public SuspeciousCarAlermInfo AlermInfo { get; set; }
        public DateTime HandleTime { get; set; }
        public ProcessBehavior ProcessBehavior { get; set; }
        public string ProcessBehaviorDescription { get { return ProcessBehavior.GetDescription(); } }

        public SuspectCarAlermHandleInfo(SuspeciousCarAlermInfo alermInfo)
        {
            AlermInfo = alermInfo;
            HandleTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[Number:{0}, Process:{1}]", AlermInfo.ReportedCarInfo.LicensePlateNumber,
                                 ProcessBehavior);
        }
    }
}
