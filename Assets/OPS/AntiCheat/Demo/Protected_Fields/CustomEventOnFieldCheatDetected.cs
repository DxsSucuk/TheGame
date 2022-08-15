using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

namespace OPS.AntiCheat.Demo.Fields
{
    public class CustomEventOnFieldCheatDetected : MonoBehaviour
    {
        // Some text element to display found cheating.
        public UnityEngine.UI.Text cheatFound;

        // Attach to the FieldCheatDetector OnFieldCheatDetected event.
        private void Start()
        {
            OPS.AntiCheat.Detector.FieldCheatDetector.OnFieldCheatDetected += Custom_OnFieldCheatDetected;
        }

        // Your custom event, what to do, if a cheat got detected.
        private void Custom_OnFieldCheatDetected()
        {
            this.cheatFound.text = "CHEAT DETECTED!";
            this.cheatFound.color = Color.red;
        }
    }
}