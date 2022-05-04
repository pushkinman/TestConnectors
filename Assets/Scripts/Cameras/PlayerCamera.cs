using System;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Cameras
{
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        public Camera Camera { get; set; }

        private void Awake()
        {
            Camera = GetComponent<Camera>();
        }
    }
}
