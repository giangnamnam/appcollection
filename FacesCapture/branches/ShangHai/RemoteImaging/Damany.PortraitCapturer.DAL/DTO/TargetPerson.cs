using DevExpress.Xpo;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class TargetPerson : CapturedImageObject
    {
        public TargetPerson()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public TargetPerson(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }



    }
}