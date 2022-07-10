# HandPoseToSpeech
Using Hand Pose Detection on an Oculus Quest 2 to detect hand gestures and output as speech.

*Note: The text-to-speech component currently only works for Windows, not the Android .apk which runs native on the Oculus Quest 2. Will work only over airlink through the Unity editor or a Windows build.*

## Current State

| Feature | Current State | Goal State | Is Achieved |
|--|--|--|--|
| Detect Hand Pose | Can detect static hand poses some of the time for all ASL alphabet signs | Can detect all basic ASL alphabet signs with reasonable accuracy | YES |
| Convert to Symbolic Textual Representation | Intermediate layer receives detection events, interpreting them and passing the interpretation as another event | Intermediate layer receives detection events, interpreting them and passing the interpretation as another event | YES |
| Text-To-Speech | Prints current letter and in-progress sentence as text and converts final message to speech | Converts letters and words to speech | YES |

## How to Use

### How to Run

1. Go to the latest releases build and download the .apk file
1. Open SideQuest or Oculus Developer Hub and attach your Oculus Quest 2 to it (ensure you allow access via USB)
1. Drag the .apk to your Oculus Quest 2 device and it should automtatically install
1. Put on your Oculus Quest 2 and open the 'Apps' menu
1. Filter by 'Unknown Source' and select this application

### How to Build

1. Download Unity 2020.3.33f1
1. Download the Android module with NDK and SDK
1. Open the Unity project in ./HandPoseToSpeech
1. Click 'File -> Build Settings' then ensure the scene you wish to build is included and the platform is 'Android'
1. Click build and choose a folder for the .apk to build to
1. Follow the 'How to Run' steps using this .apk file instead

## Architecture

### Overview

Detect Hand Pose -> Convert to Symbolic Textual Representation -> Text-To-Speech

This architecture should be modular so that alternative methods can easily be added in. Therefore superclasses will be created to match each stage:

1. Detection
1. Interpretation
1. Output

Multiple interpreters or outputs may be desired so they are triggered using events.

### Detect Hand Pose

Uses Oculus SDK to map specific hand poses (represented using manually programmed ShapeRecognizer objects) to trigger (via ShapeRecognizerActiveState objects) an event representing detection of a known hand pose.

It also uses a TransformRecognizerActiveState to add pre-conditions to the hand poses. This improves differentiation between similar hand poses and ensures they must appear realistic in orientation.

#### DetectorManager

Base class that is triggered by the 'Selector Unity Event Wrapper' function in each hand pose to trigger an event that will be sent to all 'Interpretors'.

This implementation allows for multiple interpreters to read events from a single detector.

#### Future Improvements

* Using a JointVelocityActiveState, JointRotationActiveState and/or Seqeuences to chain multiple detected poses together to recognize movement. This will allow for hand gestures that require movement to be detected e.g. 'J' or 'Z' ASL letters.

### Convert to Symbolic Textual Representation

Receives a String from the 'Detector' event upon a registered hand pose being detected that uniquely identifies that registered hand pose. *Note: There is no safety check for multiple unique poses with the same detection signature.*

Interprets the meaning of the String and triggers an event to pass on the interpreted meaning.

#### InterpretationManager

Base class that sends the letter information through to the output.

#### InterpretAsWordsManager

Interprets the incoming letters as an attempt to build a sentence and identifies words/completed sentences with timeouts.

Can send the interpretation in-progress (e.g. for visually showing the sentence being generated) or only at the end (e.g. for speaking only once the sentence is completed).

#### Future Improvements

* Using a dictionary/autocorrect to fix mistakes in gesturing words

### Text-To-Speech

Receives the interpreted meaning as a String and outputs the meaning somewhere.

The text-to-speech implementation will converts the message to audio to be played aloud.

#### OutputManager

Base class that outputs to nowhere.

#### OutputToTextManager

Outputs message to a TextPro object.

#### OutputToSpeechManager

Outputs message as audio.

## Accuracy

Some hand poses are easier to detect than others, but all can be detected.