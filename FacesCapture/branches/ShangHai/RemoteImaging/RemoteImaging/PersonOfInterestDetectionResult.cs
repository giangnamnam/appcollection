

namespace RemoteImaging
{
    public class PersonOfInterestDetectionResult
    {
        public Damany.PortraitCapturer.DAL.DTO.TargetPerson Target { get; set; }
        public Damany.Imaging.Common.Portrait  Suspect { get; set; }
        public float Similarity { get; set; }
    }
}
