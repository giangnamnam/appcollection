using System;
using System.Speech.Synthesis;


namespace RemoteImaging.LicensePlate
{
    public class SpeakService
    {
        private SpeechSynthesizer _synthesizer;
        public static string VoiceName { get; set; }
        public bool Repeat { get; set; }

        static SpeakService()
        {
            VoiceName = "Microsoft Simplified Chinese";
        }

        private SpeakService()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SelectVoice(VoiceName);

            Repeat = true;
        }

        private void Speak(string textToSpeak)
        {

            if (Repeat)
            {
                _synthesizer.SpeakCompleted +=
                    delegate { System.Diagnostics.Debug.WriteLine("complete"); };
            }

            _synthesizer.SpeakAsync(textToSpeak);

        }

        public static SpeakService Speak(string textToSpeak, bool repeat = true)
        {
            var speaker = new SpeakService();
            speaker.Repeat = repeat;

            speaker.Speak(textToSpeak);
            return speaker;
        }

        public void MuteAll()
        {
            if (_synthesizer != null)
            {
                _synthesizer.SpeakAsyncCancelAll();
            }
        }
    }
}