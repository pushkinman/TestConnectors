using System;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Input
{
    public class InputProvider : MonoBehaviour, IInputProvider
    {
        public event Action MouseDown;
        public event Action MouseUp;
        public event Action<Vector3> MousePositionChanged;

        public bool IsMouseButtonDown(int button)
        {
            return UnityEngine.Input.GetMouseButton(button);
        }

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

            if (UnityEngine.Input.GetMouseButton(0))
            {
                MousePositionChanged?.Invoke(UnityEngine.Input.mousePosition);
            }
        }
    }
}