using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// OPS - AntiCheat
using OPS.AntiCheat;
using OPS.AntiCheat.Prefs;

namespace OPS.AntiCheat.Demo.Prefs
{
    public class ProtectedPlayerPrefsDemo : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("------------------");
            Debug.Log("Protected Player Prefs Demo");
            Debug.Log("------------------");

            // Add the namespace OPS.AntiCheat.Prefs
            // Replace PlayerPrefs with ProtectedPlayerPrefs. 
            // Now access protected bools, ints, floats, strings, vector2, vector3, vector4, quaternion.

            // Bool
            ProtectedPlayerPrefs.SetBool("My Bool Key", true);
            bool protectedBoolValue = ProtectedPlayerPrefs.GetBool("My Bool Key");
            Debug.Log("Real Value: " + protectedBoolValue);

            // Int
            ProtectedPlayerPrefs.SetInt("My Int Key", 1234); //Adds a _Protected to a protected key name
            int intValue = PlayerPrefs.GetInt("My Int Key" + "_Protected"); //Because of the added _Protected, you have to read the key plus _Protected
            Debug.Log("Value saved by Unity: " + intValue);
            int protectedIntValue = ProtectedPlayerPrefs.GetInt("My Int Key");
            Debug.Log("Real Value: " + protectedIntValue);

            // Float
            ProtectedPlayerPrefs.SetFloat("My Float Key", 1234.56f);
            float floatValue = PlayerPrefs.GetInt("My Float Key" + "_Protected"); //Stored inside unity as uint
            Debug.Log("Value saved by Unity: " + floatValue);
            float protectedFloatValue = ProtectedPlayerPrefs.GetFloat("My Float Key");
            Debug.Log("Real Value: " + protectedFloatValue);

            // String
            ProtectedPlayerPrefs.SetString("My String Key", "Hello World!");
            string stringValue = PlayerPrefs.GetString("My String Key" + "_Protected");
            Debug.Log("Value saved by Unity: " + stringValue);
            string protectedStringValue = ProtectedPlayerPrefs.GetString("My String Key");
            Debug.Log("Real Value: " + protectedStringValue);

            // Vector2
            ProtectedPlayerPrefs.SetVector2("My Vector2 Key", new Vector2(123.55f, -10012.2f));
            Vector2 protectedVector2Value = ProtectedPlayerPrefs.GetVector2("My Vector2 Key");
            Debug.Log("Real Value: " + protectedVector2Value);

            // Vector3
            ProtectedPlayerPrefs.SetVector3("My Vector3 Key", new Vector3(123.55f, -10012.2f, 771123));
            Vector3 protectedVector3Value = ProtectedPlayerPrefs.GetVector3("My Vector3 Key");
            Debug.Log("Real Value: " + protectedVector3Value);

            // Vector4
            ProtectedPlayerPrefs.SetVector4("My Vector4 Key", new Vector4(1, 2, 3, 4));
            Vector4 protectedVector4Value = ProtectedPlayerPrefs.GetVector4("My Vector4 Key");
            Debug.Log("Real Value: " + protectedVector4Value);

            // Quaternion
            ProtectedPlayerPrefs.SetQuaternion("My Quaternion Key", new Quaternion(1, 2, 3, 4));
            Quaternion protectedQuaternionValue = ProtectedPlayerPrefs.GetQuaternion("My Quaternion Key");
            Debug.Log("Real Value: " + protectedQuaternionValue);
        }
    }
}