# HandPoseToSpeech
Using Hand Pose Detection on an Oculus Quest 2 to detect hand gestures and output as speech.

## Current State

| Feature | Current State | Goal State | Is Achieved |
|--|--|--|--|
| Detect Hand Pose | Can detect static hand poses some of the time for most ASL alphabet signs (excluding 'J' and 'Z') | Can detect all basic ASL alphabet signs with reasonable accuracy | NO |
| Convert to Symbolic Textual Representation | Intermediate layer receives detection events, interpreting them and passing the interpretation as another event | Intermediate layer receives detection events, interpreting them and passing the interpretation as another event | YES |
| Text-To-Speech | Only displays interpretation visually on a screen | Converts letters and words to speech | NO |

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

It uses the 'Selector Unity Event Wrapper' component to detect and send a String message to the Interpretation phase.

### Convert to Symbolic Textual Representation

Receives a String when trigggered from the 'Selector Unity Event Wrapper' function's execution upon a registered hand pose being detected that uniquely identifies that registered hand pose. *Note: There is no safety check for multiple unique poses with the same detection signature*

Interprets the meaning of the String and triggers an event to pass on the interpreted meaning.

*Note: This is only be able to convert the detected hand pose to singular/discrete symbols so more nuanced representation would be desirable. This is why it is implemented as an intermediate layer so that it can be extended to interpret compound hand gestures e.g. 'J' or 'Z'*

#### Future Improvements

* Using a JointVelocityActiveState, JointRotationActiveState and/or Seqeuences to chain multiple detected poses together to recognize movement. This will allow for hand gestures that require movement to be detected e.g. 'J' or 'Z' ASL letters.

* For ASL alphabet, chaining multiple letters should allow for the creation of words with pauses demarcating the end of words. This will be required for the conversion of text to speech in a usable manner.

### Text-To-Speech

Receives the interpreted meaning as a String and outputs the meaning somewhere.

The text-to-speech implementation will convert received Letters or Words to strings and will produce them as sound.

## Accuracy

Some hand poses are easier to detect than others, but all can be detected within ~3 attempts. This will be investigated further to determine how to differentiate similar hand signs.