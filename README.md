# HandPoseToSpeech
Using Hand Pose Detection on an Oculus Quest 2 to detect hand gestures and output as speech.

## Current State

| Feature | Current State | Goal State | Is Achieved |
|--|--|--|--|
| Detect Hand Pose | Can detect static hand poses (thumbs up and peace signs) | Can detect all basic ASL alphabet signs | NO |
| Convert to Symbolic Textual Representation | Direct conversion from detected hand pose to text manually | Middleware layer receives detection events, interpreting them and passing the interpretation as another event | NO |
| Text-To-Speech | Null | Converts letters and words to speech | NO |

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
1. Extraction of Meaning
1. Output of Meaning

### Detect Hand Pose

Uses Oculus SDK to map specific hand poses (represented using manually programmed ShapeRecognizer objects) to trigger (via ShapeRecognizerActiveState objects) an event represnting detection of a known hand pose.

It also uses a TransformRecognizerActiveState to add pre-conditions before the hand poses. This improves differentiation between similar hand poses.

#### Future Improvements

* Using a JointVelocityActiveState or chaining multiple TransformRecognizerActiveStates together to recognize movement. This will allow for hand gestures that require movement to be detected e.g. Quote gesture.

### Convert to Symbolic Textual Representation

Catches the Detect Hand Pose's "known hand pose detected" event to interpret the meaning of the hand pose and trigger another event which will pass on the interpreted meaning.

This will only be able to convert the detected hand poses to singular/discrete symbols so more nuanced representation would be desirable.

#### Future Improvements

* For ASL alphabet, chaining multiple letters should allow for the creation of words as checked against a dictionary. This will be required for the conversion of text to speech in a usable manner.

### Text-To-Speech

Catches the Convert to Symbolic Textual Representation's "meaning of detected hand pose" event to determine the correct text which will then be converted to speech.