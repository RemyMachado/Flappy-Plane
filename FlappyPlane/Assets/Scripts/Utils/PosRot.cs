using UnityEngine;

namespace Utils
{
    public struct PosRot
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public PosRot(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}