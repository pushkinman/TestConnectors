using System;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Input
{
    public class InputProvider : MonoBehaviour, IInputProvider
    {
        public event Action MouseDown;
        public event Action MouseUp;
        public event Action<Vector3> MouseStateChanged;

        public Vector3 MousePosition => UnityEngine.Input.mousePosition;

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                MouseDown?.Invoke();
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                MouseUp?.Invoke();
            }

            if (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonUp(0) ||
                UnityEngine.Input.GetMouseButton(0))
                MouseStateChanged?.Invoke(UnityEngine.Input.mousePosition);
        }

        public bool GetMouseButtonDown(int button)
        {
            return UnityEngine.Input.GetMouseButtonDown(button);
        }

        public bool GetMouseButtonUp(int button)
        {
            return UnityEngine.Input.GetMouseButtonUp(button);
        }

        public bool GetMouseButton(int button)
        {
            return UnityEngine.Input.GetMouseButton(button);
        }
    }
}