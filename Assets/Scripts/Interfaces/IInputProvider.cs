using System;
using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IInputProvider
    {
        event Action<Vector2> PlayerMoved;
        event Action PlayerJumped;
        event Action<bool> FreeLookEnabled;
    }
}