using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Repositories
{
    public class AzureCognitiveHelper
    {
        SpeechConfig speechConfig;
        public AzureCognitiveHelper(string speechKey, string speechRegion)
        {
            speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechRecognitionLanguage = "en-US";
        }

        public async Task<string> ConvertAudioToText(string filepath)
        {
            using var audioConfig = AudioConfig.FromWavFileInput(filepath);
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            return OutputSpeechRecognitionResult(speechRecognitionResult);
        }
        private string OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    return speechRecognitionResult.Text;
                case ResultReason.NoMatch:
                    return $"NOMATCH: Speech could not be recognized.";

                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    return $"CANCELED: Reason={cancellation.Reason}";

                default:
                    return "Sorry We couldn't process your audio";
            }
        }
    }
}
