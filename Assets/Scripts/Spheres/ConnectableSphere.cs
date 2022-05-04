using System;
using TestConnectors.Interfaces;
using TestConnectors.Platform;
using UnityEngine;

namespace TestConnectors.Spheres
{
    public class ConnectableSphere : MonoBehaviour
    {
        [SerializeField] private MovablePlatform movablePlatform;

        public GameObject selectedObject;
        Vector3 offset;

        private void Awake()
        {
            movablePlatform.PositionChanged += Move;
        }

        private void Move(Vector2 value)
        {
            Debug.Log("PositionChanged");
        }
    }
}