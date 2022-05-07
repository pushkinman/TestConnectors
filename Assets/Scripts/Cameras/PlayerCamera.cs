﻿using System;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;

namespace TestConnectors.Cameras
{
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        private IInputProvider _inputProvider;
        
        public Camera Camera { get; set; }
        public Transform CursorTransform { get; set; }

        private void Awake()
        {
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            Camera = GetComponent<Camera>();
            CursorTransform = new GameObject().transform;
        }

        private void Update()
        {
            var mousePosition = _inputProvider.MousePosition;
            CursorTransform.position = Camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y,
                Camera.transform.position.y)) + new Vector3(0,+ ProjectSettings.SphereOffset, 0);
        }
    }
}