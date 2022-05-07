using System;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;

namespace TestConnectors.Cameras
{
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        public Camera Camera { get; set; }
        public Transform CursorTransform { get; set; }

        private IInputProvider _inputProvider;

        private void Awake()
        {
            Camera = GetComponent<Camera>();
            CursorTransform = new GameObject().transform;
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
        }

        private void Update()
        {
            var mousePosition = _inputProvider.MousePosition;
            CursorTransform.position = Camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y,
                Camera.transform.position.y)) + new Vector3(0,+ ProjectSettings.SphereOffset, 0);
        }
    }
}