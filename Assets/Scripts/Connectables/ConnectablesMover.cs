using System;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Connectable
{
    public class ConnectablesMover : MonoBehaviour, IConnectablesMover
    {
        private MovablePlatform _selectedPlatform;

        public IInputProvider _inputProvider;
        public IPlayerCamera _playerCamera;
        
        private void Awake()
        {
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            _playerCamera = CompositionRoot.GetPlayerCamera();
        }

        private void Start()
        {
            _inputProvider.MouseDown += CheckIfPlatformCanBeSelected;
            _inputProvider.MouseUp += DeselectMovablePlatform;
            _inputProvider.MouseStateChanged += TryMoveConnectable;
        }

        private void OnDestroy()
        {
            _inputProvider.MouseDown -= CheckIfPlatformCanBeSelected;
            _inputProvider.MouseUp -= DeselectMovablePlatform;
            _inputProvider.MouseStateChanged -= TryMoveConnectable;
        }

        private void CheckIfPlatformCanBeSelected()
        {
            var ray = _playerCamera.Camera.ScreenPointToRay(_inputProvider.MousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var objectHit = hit.transform;
                _selectedPlatform = objectHit.GetComponent<MovablePlatform>();
            }
        }
        
        private void TryMoveConnectable(Vector3 value)
        {
            if (_selectedPlatform == null) return;

            var connectable = _selectedPlatform.transform.parent;

            connectable.position =
                _playerCamera.Camera.ScreenToWorldPoint(new Vector3(value.x, value.y,
                    _playerCamera.Camera.transform.position.y));
        }

        private void DeselectMovablePlatform()
        {
            _selectedPlatform = null;
        }
    }
}