using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about what to output and does so
    /// </summary>
    public class OutputManager : MonoBehaviour
    {
        [SerializeField]
        protected InterpretationManager interpretationManager;  // Which interpretation to listen to

        protected void Start()
        {
            // Listen to OnInterpretationEvent and assign proper response
            interpretationManager.OnInterpretationEvent += ReceiveInterpretationEvent;
        }

        /// <summary>
        /// Receive a detect event indicating the Shape Recognizer and/or Transformer Recognizer has been activated
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        protected virtual void ReceiveInterpretationEvent(string message)
        {
            return;
        }
    }
}
