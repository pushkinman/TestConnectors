using System;
using TestConnectors.Interfaces;
using TestConnectors.Platform;
using UnityEngine;

namespace TestConnectors.Spheres
{
    public class Connectable : MonoBehaviour
    {
        [SerializeField] private MovablePlatform movablePlatform;

        private bool isSelected;
        private IInputProvider _inputProvider;
        private Camera _camera;

        private void Awake()
        {
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            _camera = CompositionRoot.GetPlayerCamera().Camera;
        }

        private void Start()
        {
            _inputProvider.MousePositionChanged += MoveConnectable;
            _inputProvider.MouseDown += CheckIfPlatformShouldBeSelected;
            _inputProvider.MouseUp += DeselectPlatform;
        }
        
        private void MoveConnectable(Vector3 value)
        {
            if (isSelected)
            {
                gameObject.transform.position =
                    _camera.ScreenToWorldPoint(new Vector3(value.x, value.y, _camera.transform.position.y));
            }
        }

        private void CheckIfPlatformShouldBeSelected()
        {
            if (movablePlatform.IsCursorOverPlatform)
            {
                isSelected = true;
            }
        }

        private void DeselectPlatform()
        {
            isSelected = false;
        }
    }
}