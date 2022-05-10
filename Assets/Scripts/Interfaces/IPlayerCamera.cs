using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IPlayerCamera
    {
        Camera Camera { get; }
        Transform CursorTransform { get; }
    }
}