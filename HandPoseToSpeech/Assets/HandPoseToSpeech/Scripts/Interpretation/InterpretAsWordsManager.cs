using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives a detection and tries to build words from it
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
        /// Receive a detect event indicating the Shape Recognizer and/or Transformer Recognizer has been activated
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        protected override void ReceiveDetectionEvent(string message)
        {
            // Cancel creating sentence if new word detected
            if (IsInvoking("CreateSentence"))
            {
                builtString += " ";
                CancelInvoke("CreateSentence");
            }

            // Add to string
            builtString += message;

            // Send message incrementally
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
