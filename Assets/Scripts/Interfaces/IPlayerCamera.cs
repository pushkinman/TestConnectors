using UnityEngine;

namespace TestConnectors.Interfaces
{
    public interface IPlayerCamera
    {
        Camera Camera { get; set; }
        Transform CursorTransform { get; set; }
    }
}