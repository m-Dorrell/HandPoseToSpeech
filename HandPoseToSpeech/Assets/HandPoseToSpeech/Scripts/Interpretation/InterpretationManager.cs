using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives a message and interprets its meaning
    /// </summary>
    public class InterpretationManager : MonoBehaviour
    {
        [SerializeField]
        protected DetectionManager detectionManager;  // Which detection to listen to

        public event Action<string> OnInterpretationEvent;

        protected void Start()
        {
            // Listen to OnDetectionEvent and assign proper response
            detectionManager.OnDetectionEvent += ReceiveDetectionEvent;
        }

        /// <summary>
        /// Receive a detect event indicating the Shape Recognizer and/or Transformer Recognizer has been activated
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        protected virtual void ReceiveDetectionEvent(string message)
        {
            OnInterpretationEvent?.Invoke(message);
        }
    }
}
