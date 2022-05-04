using System;
using UnityEngine;

namespace TestConnectors.Platform
{
    public class MovablePlatform
    {
        public event Action<Vector2> PositionChanged;

        private void OnMouseDown()
        {
            PositionChanged?.Invoke(new Vector2(2,2));
        }
    }
}