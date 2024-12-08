using UnityEngine;
using System;

namespace Serialization
{

    [Serializable]
    public class Vector
    {
        public float X;
        public float Y;
        public float Z;

        [Newtonsoft.Json.JsonConstructor]
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(Vector vector) => new Vector3(vector.X, vector.Y, vector.Z);
        public static implicit operator Vector(Vector3 vector) => new Vector(vector.x, vector.y, vector.z);
    }
}
