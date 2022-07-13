using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Triggers a detection event
    /// </summary>
    public class DetectionManager : MonoBehaviour
    {
        public event Action<string> OnDetectionEvent;

        /// <summary>
        /// Function to catch an Oculus detection event indicating the Shape Recognizer and/or Transformer Recognizer has been activated
        /// </summary>
        /// <param name="message">A message indicating what was detected</param>
        public void ReceiveDetectEvent(string message)
        {
            OnDetectionEvent?.Invoke(message);
        }
    }
}
