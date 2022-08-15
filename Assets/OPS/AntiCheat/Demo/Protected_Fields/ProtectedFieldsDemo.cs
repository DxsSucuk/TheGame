using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OPS - AntiCheat
using OPS.AntiCheat;
using OPS.AntiCheat.Field;

namespace OPS.AntiCheat.Demo.Field
{
    public class ProtectedFieldsDemo : MonoBehaviour
    {
        // Inspector
        public ProtectedBool ProtectedBool;
        public ProtectedDouble ProtectedDouble;
        public ProtectedFloat ProtectedFloat;
        public ProtectedDecimal ProtectedDecimal;
        public ProtectedInt32 ProtectedInt32;
        public ProtectedInt64 ProtectedInt64;
        public ProtectedUInt32 ProtectedUInt32;
        public ProtectedUInt64 ProtectedUInt64;
        public ProtectedString ProtectedString;
        public ProtectedVector2 ProtectedVector2;
        public ProtectedVector2Int ProtectedVector2Int;
        public ProtectedVector3 ProtectedVector3;
        public ProtectedVector3Int ProtectedVector3Int;
        public ProtectedVector4 ProtectedVector4;
        public ProtectedQuaternion ProtectedQuaternion;

        /// <summary>
        /// Logging example over all protected types.
        /// </summary>
        private void Start()
        {
            Debug.Log("------------------");
            Debug.Log("Protected Fields Demo");
            Debug.Log("------------------");

            // Add the namespace OPS.AntiCheat.Field
            // Replace your field type with a protected one. (Most types are supported, so just add a Protected in front.)
            // Now access the protected value of a field, with the property Value.

            // UInt
            ProtectedUInt16 a = new ProtectedUInt16(1234);
            a += 1;
            Debug.Log(a);
            ProtectedUInt32 b = new ProtectedUInt32(5678);
            b += 2;
            Debug.Log(b);
            ProtectedUInt64 c = new ProtectedUInt64(91011);
            c += 3;
            Debug.Log(c);

            // Int
            ProtectedInt16 d = new ProtectedInt16(1234);
            d += 1;
            Debug.Log(d);
            ProtectedInt32 e = new ProtectedInt32(5678);
            e += 2;
            Debug.Log(e);
            ProtectedInt64 f = new ProtectedInt64(91011);
            f += 3;
            Debug.Log(f);

            // Float
            ProtectedFloat g = new ProtectedFloat(1234.123f);
            g += 0.11f;
            Debug.Log(g);

            // Double
            ProtectedDouble h = new ProtectedDouble(1234.123d);
            h += 0.11d;
            Debug.Log(h);

            // String
            ProtectedString i = new ProtectedString("My Protected Text");
            i += "!!";
            Debug.Log(i);

            // Vector
            ProtectedVector2 j = new ProtectedVector2(new Vector2(1, 2));
            j += new Vector2(1, 0);
            Debug.Log(j);

            ProtectedVector3 k = new ProtectedVector3(new Vector3(1, 2, 3));
            k += new Vector3(2, 1, 0);
            Debug.Log(k);

            ProtectedVector4 l = new ProtectedVector4(new Vector4(1, 2, 3, 4));
            l += new Vector4(3, 2, 1, 0);
            Debug.Log(l);

            // Quaternion
            ProtectedQuaternion m = new ProtectedQuaternion(new Quaternion(1, 2, 3, 4));
            m *= new Quaternion(3, 2, 1, 0);
            Debug.Log(m);

            // Bool
            ProtectedBool n = new ProtectedBool(false);
            n = false;
            Debug.Log(n);

            // Decimal
            ProtectedDecimal o = new ProtectedDecimal(12312.534m);
            o += 0.11m;
            Debug.Log(o);

            // DateTime
            ProtectedDateTime p = new ProtectedDateTime(System.DateTime.Now);
            p = p.Value.AddDays(5);
            Debug.Log(p);
        }
    }
}