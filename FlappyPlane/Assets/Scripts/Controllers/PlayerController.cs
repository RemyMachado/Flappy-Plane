using System;
using Managers;
using Player;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 GetDistanceFrom(Vector3 fromPosition)
        {
            return transform.position - fromPosition;
        }
    }
}