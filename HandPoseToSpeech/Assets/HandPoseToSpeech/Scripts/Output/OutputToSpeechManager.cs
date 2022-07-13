using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about meaning of signs and output meaning as speech
    /// </summary>
    public class OutputToSpeechManager : OutputManager
    {
        [SerializeField]
        private TTSSpeaker speaker;

        protected new void Start()
        {
            base.Start();
            TTS.Init();  // Initialize text-to-speech package
        }

        /// <summary>
        /// Receive an interpretation event and output the message as speech
        /// </summary>
        /// <param name="message">A message indicating the meaning of the event(s) that was/were triggered</param>
        protected override void ReceiveInterpretationEvent(string message)
        {
            TTS.SayAsync(message, speaker);
        }
    }
}
