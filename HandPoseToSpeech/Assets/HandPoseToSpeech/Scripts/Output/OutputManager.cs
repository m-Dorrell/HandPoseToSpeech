using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about meaning of signs and output meaning
    /// </summary>
    public class OutputManager : MonoBehaviour
    {
        [SerializeField]
        protected InterpretationManager interpretationManager;  // Which interpretation manager to listen to

        protected void Start()
        {
            interpretationManager.OnInterpretationEvent += ReceiveInterpretationEvent;
        }

        /// <summary>
        /// Receive an interpretation event
        /// </summary>
        /// <param name="message">A message indicating the meaning of the event(s) that was/were triggered</param>
        protected virtual void ReceiveInterpretationEvent(string message)
        {
            return;
        }
    }
}
