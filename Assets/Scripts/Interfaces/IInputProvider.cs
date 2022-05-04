using System;
using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IInputProvider
    {
        event Action MouseDown;
        event Action MouseUp;
        event Action<Vector3> MousePositionChanged;

        bool IsMouseButtonDown(int button);
    }
}