using UnityEngine;

namespace TestConnectors.Connectable
{
    public class SelectableSphere : MonoBehaviour
    {
        public bool IsCursorOverSphere { get; private set; }

        private void OnMouseEnter()
        {
            IsCursorOverSphere = true;
        }

        private void OnMouseExit()
        {
            IsCursorOverSphere = false;
        }
    }
}