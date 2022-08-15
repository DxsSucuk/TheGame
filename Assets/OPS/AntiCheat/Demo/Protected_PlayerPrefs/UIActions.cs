using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// Unity
using UnityEngine;

// OPS - AntiCheat
using OPS.AntiCheat.Prefs;

namespace OPS.AntiCheat.Demo.Prefs
{
    public class UIActions : MonoBehaviour
    {
        // The example protected player prefs key.
        private const string CPlayerPrefKey = "Test_Data";

        // Some input element to create/update the player prefs value.
        public UnityEngine.UI.InputField inputField_Value;

        // Some text element to display the read player prefs value.
        public UnityEngine.UI.Text text_Value_Stored;

        // Some text element to display the read protected player prefs value.
        public UnityEngine.UI.Text text_Value;

        /// <summary>
        /// Assign the input fields value to the protected PlayerPrefs.
        /// </summary>
        public void AssignValue()
        {
            ProtectedPlayerPrefs.SetString(CPlayerPrefKey, this.inputField_Value.text);
        }

        /// <summary>
        /// Read from the protected PlayerPrefs.
        /// </summary>
        public void ReadValue()
        {
            this.text_Value_Stored.text = "Stored value: " + PlayerPrefs.GetString(CPlayerPrefKey + "_Protected");
            this.text_Value.text = "Protected read value: " + ProtectedPlayerPrefs.GetString(CPlayerPrefKey);
        }
    }
}