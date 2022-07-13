using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about meaning of signs and output meaning as text to a TextMeshPro object
    /// </summary>
    public class OutputToTextManager : OutputManager
    { 
        [SerializeField]
        private TextMeshPro textMeshPro;  // Visible text

        protected new void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Receive an interpretation event and output the message as speech
        /// </summary>
        /// <param name="message">A message indicating the meaning of the event(s) that was/were triggered</param>
        protected override void ReceiveInterpretationEvent(string message)
        {
            textMeshPro.text = message;
        }
    }
}
