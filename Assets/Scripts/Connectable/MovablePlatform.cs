using UnityEngine;

namespace TestConnectors.Connectable
{
    public class MovablePlatform : MonoBehaviour
    {
        public bool IsCursorOverPlatform { get; private set; }

        private void OnMouseEnter()
        {
            IsCursorOverPlatform = true;
        }

        private void OnMouseExit()
        {
            IsCursorOverPlatform = false;
        }
    }
}