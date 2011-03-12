

namespace Damany.Imaging.Common
{
    public class PersonOfInterestDetectionResult
    {
        public System.Drawing.Image Target { get; set; }
        public System.Drawing.Image Suspect { get; set; }
        public float Similarity { get; set; }
    }
}
