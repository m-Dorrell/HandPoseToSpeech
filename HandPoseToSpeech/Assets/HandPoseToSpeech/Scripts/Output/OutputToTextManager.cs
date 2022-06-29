using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandPoseToSpeech
{
    /// <summary>
    /// Receives information about what to output and does so
    /// </summary>
    public class OutputToTextManager : OutputManager
    { 
        [SerializeField]
        private TextMeshPro textMeshPro;  // A text to update visibly

        protected new void Start()
        {
            base.Start();
        }

        /// <summary>
        /// Print the interpret event to the screen
        /// </summary>
        /// <param name="message">A message indicating the event that was triggered</param>
        protected override void ReceiveInterpretationEvent(string message)
        {
            textMeshPro.text = message;
        }
    }
}
