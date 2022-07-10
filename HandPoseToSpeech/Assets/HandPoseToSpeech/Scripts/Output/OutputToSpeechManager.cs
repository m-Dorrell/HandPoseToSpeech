using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityLibrary;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about what to output and does so
    /// </summary>
    public class OutputToSpeechManager : OutputManager
    {
        [SerializeField]
        private AudioSource source;

        protected new void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Speak the message
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        protected override void ReceiveInterpretationEvent(string message)
        {
            Debug.Log(message);
            //Speech.instance.Say(message, TTSCallback);
        }

        /*void TTSCallback(string message, AudioClip audio)
        {
            source.clip = audio;
            source.Play();
        }*/
    }
}
