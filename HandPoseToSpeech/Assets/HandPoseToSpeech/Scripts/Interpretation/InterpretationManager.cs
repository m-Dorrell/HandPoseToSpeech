using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives a message and interprets its meaning
    /// Sends the meaning out as an event
    /// </summary>
    public class InterpretationManager : MonoBehaviour
    {
        [SerializeField]
        protected DetectionManager detectionManager;  // Which detection manager to listen to

        public event Action<string> OnInterpretationEvent;

        protected void Start()
        {
            detectionManager.OnDetectionEvent += ReceiveDetectionEvent;
        }

        /// <summary>
        /// Receive a detect event and pass along the message
        /// </summary>
        /// <param name="message">A message indicating what was detected</param>
        protected virtual void ReceiveDetectionEvent(string message)
        {
            OnInterpretationEvent?.Invoke(message);
        }
    }
}
