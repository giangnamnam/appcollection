using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class SuspectCarFormPresenter : INotifySuspeciousLicensePlate
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly LicensePlateRepository _licensePlateRepository;
        private SuspeciousCarAlermInfo _currentAlerm;
        private SpeakService _speaker;

        public SuspectCarFormPresenter(IMessageBoxService messageBoxService,
                                       ILicensePlateEventPublisher licensePlateEventPublisher,
                                       LicensePlateRepository licensePlateRepository)
        {
            if (messageBoxService == null) throw new ArgumentNullException("messageBoxService");
            if (licensePlateRepository == null) throw new ArgumentNullException("licensePlateRepository");

            _messageBoxService = messageBoxService;
            _licensePlateRepository = licensePlateRepository;
        }

        public void Notify(SuspeciousCarAlermInfo alerm)
        {
            _currentAlerm = alerm;

            SoundVoiceAlerm(_currentAlerm);

            Func<System.Windows.Forms.Form> doit = () =>
                                                       {
                                                           var form = new FormSuspeciousCar();
                                                           form.DataContext = _currentAlerm;

                                                           form.AttachPresenter(this);
                                                           return form;
                                                       };
            _messageBoxService.ShowForm(doit);

        }

        private void SoundVoiceAlerm(SuspeciousCarAlermInfo alerm)
        {
            var textToSpeak = string.Format(Properties.Settings.Default.VoiceAlertTemplate,
                                            alerm.CapturedLicenseInfo.LicensePlateNumber);

            _speaker = SpeakService.Speak(textToSpeak);
        }

        public void Mute()
        {
            if (_speaker != null)
            {
                _speaker.MuteAll();
            }
        }

        public void ConfirmAlarm()
        {
            SuspectCarAlermHandleInfo suspectCarHandleInfo = CreateSuspectCarHandleInfo();
            suspectCarHandleInfo.ProcessBehavior = ProcessBehavior.Confirmed;

            System.Diagnostics.Debug.WriteLine(suspectCarHandleInfo.ToString());

            _licensePlateRepository.Save(suspectCarHandleInfo);


        }

        private SuspectCarAlermHandleInfo CreateSuspectCarHandleInfo()
        {
            return new SuspectCarAlermHandleInfo(_currentAlerm);
        }

        public void DiscardAlerm()
        {
            var suspectCarHandleInfo = CreateSuspectCarHandleInfo();
            suspectCarHandleInfo.ProcessBehavior = ProcessBehavior.Ignored;

            System.Diagnostics.Debug.WriteLine(suspectCarHandleInfo.ToString());
            _licensePlateRepository.Save(suspectCarHandleInfo);


        }
    }
}
