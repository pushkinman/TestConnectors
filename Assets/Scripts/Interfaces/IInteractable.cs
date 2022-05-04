using System;
using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IInteractable
    {
        event Action<Vector2> PositionChanged;
    }
}