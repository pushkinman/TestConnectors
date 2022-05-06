using System;
using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IInputProvider
    {
        event Action MouseDown;
        event Action MouseUp;
        event Action<Vector3> MousePositionChanged;

        Vector3 MousePosition { get; }
        bool IsMouseButtonDown(int button);
    }
}