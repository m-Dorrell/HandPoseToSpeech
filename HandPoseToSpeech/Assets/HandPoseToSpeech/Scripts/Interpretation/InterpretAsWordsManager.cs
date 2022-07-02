using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives a detection and tries to build words from it
    /// </summary>
    public class InterpretAsWordsManager : MonoBehaviour
    {
        [SerializeField]
        protected DetectionManager detectionManager;  // Which detection to listen to

        private string word = "";

        protected void Start()
        {
            // Listen to OnDetectionEvent and assign proper response
            detectionManager.OnDetectionEvent += ReceiveDetectionEvent;
        }

        public event Action<string> OnInterpretationEvent;

        /// <summary>
        /// Receive a detect event indicating the Shape Recognizer and/or Transformer Recognizer has been activated
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        public void ReceiveDetectionEvent(string message)
        {
            OnInterpretationEvent?.Invoke(message);
        }
    }
}
