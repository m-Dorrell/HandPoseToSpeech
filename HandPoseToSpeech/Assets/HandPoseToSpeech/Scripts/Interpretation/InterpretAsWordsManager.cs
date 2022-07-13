using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives a detection and tries to build words from it
    /// Sends the meaning out as an event
    /// </summary>
    public class InterpretAsWordsManager : InterpretationManager
    {
        [SerializeField]
        private float secondsToWord = 5.0f;  // How long to wait between inputs to define a word
        [SerializeField]
        private float secondsToEnd = 10.0f;  // How long to wait between inputs to define end of input
        [SerializeField]
        private bool sendInProgress = false;  // Whether the progress should be sent incrementally or only at the end

        private string builtString = "";  // Holds the interpreted input

        protected new void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Receive a detect event and build a word then pass the message along
        /// </summary>
        /// <param name="message">A message indicating what was detected</param>
        protected override void ReceiveDetectionEvent(string message)
        {
            // Cancel creating sentence if new word detected
            if (IsInvoking("CreateSentence"))
            {
                builtString += " ";
                CancelInvoke("CreateSentence");
            }

            // Add message to string
            builtString += message;

            // Send built sentence incrementally
            if (sendInProgress)
            {
                base.ReceiveDetectionEvent(builtString);
            }

            // Cancel creating word if new word detected
            if (IsInvoking("CreateWord"))
                CancelInvoke("CreateWord");
            
            // Trigger countdown to word being finalised
            Invoke("CreateWord", secondsToWord);
        }

        /// <summary>
        /// Triggers the countdown to create a sentence and complete the input
        /// </summary>
        private void CreateWord()
        {
            // Trigger countdown to sentence being finalised
            Invoke("CreateSentence", secondsToEnd-secondsToWord);
        }

        /// <summary>
        /// Sends the final input if desired and resets the string
        /// </summary>
        private void CreateSentence()
        {
            // Send message only at end
            if (!sendInProgress)
            {
                base.ReceiveDetectionEvent(builtString);
            }

            // Reset string
            builtString = "";
        }
    }
}
