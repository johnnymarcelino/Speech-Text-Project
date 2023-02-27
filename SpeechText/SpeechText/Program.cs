//
// For more samples please visit https://github.com/Azure-Samples/cognitive-services-speech-sdk 
// 

// Creates an instance of a speech config with specified subscription key and service region.
using Windows.Media.SpeechSynthesis;

string subscriptionKey = "undefined";
string subscriptionRegion = "undefined";

var config = SpeechConfig.FromSubscription(subscriptionKey, subscriptionRegion);
// Note: the voice setting will not overwrite the voice element in input SSML.
config.SpeechSynthesisVoiceName = "pt-BR-DonatoNeural";

string text = "Oi, esta é Donato.";

// use the default speaker as audio output.
using (var synthesizer = new SpeechSynthesizer(config))
{
    using (var result = await synthesizer.SpeakTextAsync(text))
    {
        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            Console.WriteLine($"Speech synthesized for text [{text}]");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

            if (cancellation.Reason == CancellationReason.Error)
            {
                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                Console.WriteLine($"CANCELED: Did you update the subscription info?");
            }
        }
    }
}
