////https://mdn.github.io/web-speech-api/speak-easy-synthesis/

var textToSpeech = (function () {
    var synth = window.speechSynthesis;
    var voices = [];
    function speak(text) {
        if (!text)
            return;

        if (synth.speaking) {
            console.error('speechSynthesis.speaking');
            return;
        }
        if (true) {
            var utterThis = new SpeechSynthesisUtterance(text);
            utterThis.onend = function (event) {
                console.log('SpeechSynthesisUtterance.onend');
            }
            utterThis.onerror = function (event) {
                console.error('SpeechSynthesisUtterance.onerror');
            }
            var selectedOption = 'Microsoft Mark - English (United States) (en-US)'; // voiceSelect.selectedOptions[0].getAttribute('data-name');
            for (i = 0; i < voices.length; i++) {
                if (voices[i].name === selectedOption) {
                    utterThis.voice = voices[i];
                    break;
                }
            }
            utterThis.pitch = 1; // pitch.value;
            utterThis.rate = 1; // rate.value;
            synth.speak(utterThis);
        }
    }

    return {
        speak,
    }
})();