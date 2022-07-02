using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives a message and interprets its meaning
    /// </summary>
    public class DetectionManager : MonoBehaviour
    {
        public event Action<string> OnDetectionEvent;

        /// <summary>
        /// Receive a detect event indicating the Shape Recognizer and/or Transformer Recognizer has been activated
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        public void ReceiveDetectEvent(string message)
        {
            OnDetectionEvent?.Invoke(message);
        }
    }
}
