using System;
using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IInputProvider
    {
        event Action MouseDown;
        event Action MouseUp;
        event Action<Vector3> MouseStateChanged;

        Vector3 MousePosition { get; }
        bool GetMouseButtonDown(int button);
        bool GetMouseButtonUp(int button);
        bool GetMouseButton(int button);
    }
}