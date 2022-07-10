using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadSpeaker;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about what to output and does so
    /// </summary>
    public class OutputToSpeechManagerTTS : OutputManager
    {
        //[SerializeField]
        //private AudioSource source;
        public TTSSpeaker speaker;

        protected new void Start()
        {
            base.Start();
            TTS.Init();
        }

        /// <summary>
        /// Speak the message
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        protected override void ReceiveInterpretationEvent(string message)
        {
            Debug.Log(message);
            TTS.SayAsync(message, speaker);
        }
    }
}
