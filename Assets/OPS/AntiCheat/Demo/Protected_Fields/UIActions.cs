using System.Collections;
using System.Collections.Generic;
using System.Reflection;

// Unity
using UnityEngine;

// OPS - AntiCheat
using OPS.AntiCheat.Field;

namespace OPS.AntiCheat.Demo.Field
{
    public class UIActions : MonoBehaviour
    {
        // The protected example string variable.
        public ProtectedString variable_ProtectedString;

        // Some text element to display the variables real value.
        public UnityEngine.UI.Text text_RealValue;

        // Some text element to display the variables fake (honeypot) value.
        public UnityEngine.UI.Text text_FakeValue;

        // Some input element to create the variables value.
        public UnityEngine.UI.InputField inputField_Value;

        /// <summary>
        /// Assign the input fields value to the protected variable.
        /// </summary>
        public void AssignRealValue()
        {
            variable_ProtectedString = this.inputField_Value.text;
        }

        /// <summary>
        /// Assign the input fields value to the protected variables honeypot.
        /// This happens by accessing the memory through reflection how cheats would do.
        /// </summary>
        public void AssignCheatedValue()
        {
            FieldInfo fieldInfo = this.variable_ProtectedString.GetType().GetField("fakeValue", BindingFlags.Instance | BindingFlags.NonPublic);
            object boxed = this.variable_ProtectedString;
            fieldInfo.SetValue(boxed, this.inputField_Value.text);
            this.variable_ProtectedString = (ProtectedString)boxed;
        }

        /// <summary>
        /// Update the text elements.
        /// </summary>
        private void Update()
        {
            // Real value
            this.text_RealValue.text = "Real value: " + this.variable_ProtectedString.Value;

            // Fake (Honeypot) value
            FieldInfo fieldInfo = this.variable_ProtectedString.GetType().GetField("fakeValue", BindingFlags.Instance | BindingFlags.NonPublic);
            this.text_FakeValue.text = "Fake (honeypot) value: " + fieldInfo.GetValue(this.variable_ProtectedString).ToString();
        }
    }
}