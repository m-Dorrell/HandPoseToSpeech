# HandPoseToSpeech
Using Hand Pose Detection on an Oculus Quest 2 to detect hand gestures and output as speech.

| Demo | Link |
|--|--|
| Hello World Demo | [![Hello World Demo Link](https://img.youtube.com/vi/AwRV_NDzUMU/0.jpg)](https://www.youtube.com/watch?v=AwRV_NDzUMU) |
| Full Alphabet Demo | [![Full Alphabet Demo Link](https://img.youtube.com/vi/Gwr927PJszo/0.jpg)](https://www.youtube.com/watch?v=Gwr927PJszo) |

## Current State

| Feature | Current State | Goal State | Is Achieved |
|--|--|--|--|
| Detect Hand Pose | Can detect static hand poses some of the time for all ASL alphabet signs | Can detect all basic ASL alphabet signs with reasonable accuracy | YES |
| Convert to Symbolic Textual Representation | Intermediate layer sends detection events, interprets & sends the meaning as an event, and outputs the meaning | Intermediate layer receives detection events, interpreting them and passing the interpretation as another event | YES |
| Text-To-Speech | Prints current letter and in-progress sentence as text then converts final message to speech. Demarcates work words and sentences with pauses. | Converts letters and words to speech | YES |

## How to Use

### How to Run

1. Go to the latest releases build and download the .apk file
1. Open SideQuest or Oculus Developer Hub and attach your Oculus Quest 2 to it (ensure you allow access via USB)
1. Drag the .apk to your Oculus Quest 2 device and it should automtatically install
1. Put on your Oculus Quest 2 and open the 'Apps' menu
1. Filter by 'Unknown Source' and select this application

### How to Build

1. Download Unity 2020.3.33f1
1. Download the Android Unity module with NDK and SDK
1. Open the Unity project in ./HandPoseToSpeech
1. Click 'File -> Build Settings' then ensure the scene you wish to build is included and the platform is 'Android'
1. Click build and choose a folder for the .apk to build to
1. Follow the 'How to Run' steps using this .apk file instead

## Architecture

### Overview

Detect Hand Pose -> Convert to Symbolic Textual Representation -> Text-To-Speech

This architecture is modular so that alternative methods can easily be added in. Therefore superclasses have been created to match each stage:

1. Detection
1. Interpretation
1. Output

Multiple interpreters or outputs may be desired so they are triggered using events.

### Detect Hand Pose

Uses Oculus SDK to map specific hand poses (represented using manually programmed ShapeRecognizer objects) to trigger (via ShapeRecognizerActiveState objects) an event representing detection of a known hand pose. It also uses a TransformRecognizerActiveState to add pre-conditions to the hand poses. This improves differentiation between similar hand poses and ensures they must appear realistic in orientation.

The 'J' letter uses a 'Sequence' to identify the start and end parts of the sign.
*Note: This doesn't require the movement to be accurate so could be improved in the future by requiring passing through intermediary stages.*

The 'Z' letter triggers an invisible sphere collider to appear and move with the sign to ensure the user is making a 'Z' motion.
*Note: A 'reset' should be added if the user stops part way as it requires the full motion to be used.*

#### DetectorManager

Base class that is triggered by the 'Selector Unity Event Wrapper' function in each hand pose to trigger an event that will be sent to all 'Interpretors'.

This implementation allows for multiple interpreters to read events from a single detector.

### Convert to Symbolic Textual Representation

Receives a String from the 'Detector' event upon a registered hand pose being detected that uniquely identifies that registered hand pose. *Note: There is no safety check for multiple unique poses with the same detection signature.*

Interprets the meaning of the String and triggers an event to pass on the interpreted meaning.

#### InterpretationManager

Base class that sends the letter information through to the output.

#### InterpretAsWordsManager

Interprets the incoming letters as an attempt to build a sentence and identifies words/completed sentences with timeouts.

Can send the interpretation in-progress (e.g. for visually showing the sentence being generated) or only at the end (e.g. for speaking only once the sentence is completed).

*Note: The default timeout is 4 seconds to build a word and 8 seconds to end a sentence*

#### Future Improvements

* Using a dictionary/autocorrect to fix mistakes in gesturing words

### Text-To-Speech

Receives the interpreted meaning as a String and outputs the meaning somewhere.

#### OutputManager

Base class that outputs to nowhere.

#### OutputToTextManager

Outputs message to a TextPro object.

#### OutputToSpeechManager

Outputs message as audio using the ReadSpeaker plugin.

## Accuracy

Some hand poses are easier to detect than others, but all can be detected.

## Acknowledgements

Big thank you to [ReadSpeaker](https://www.readspeaker.com/) for assisting me with using their text-to-speech library on the Oculus Quest 2.

Thanks to Meta & Oculus for developing the Quest 2 headset and Unity Oculus SDK that allows for the development of applications like this.