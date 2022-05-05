using System;
using TestConnectors.Enums;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Connectable
{
    public class Connectable : MonoBehaviour
    {
        [SerializeField] private MovablePlatform movablePlatform;
        [SerializeField] private SelectableSphere selectableSphere;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material awaitingMaterial;
        
        private Material _defaultSphereMaterial;
        private MeshRenderer _sphereMeshRenderer;
        
        private bool _isMovablePlatformSelected;
        private bool _isSelectableSphereSelected;

        private IInputProvider _inputProvider;
        private Camera _camera;
        private IConnectablesManager _connectablesManager;

        private void Awake()
        {
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            _camera = CompositionRoot.GetPlayerCamera().Camera;
            _connectablesManager = CompositionRoot.GetSpawner();

            _sphereMeshRenderer = selectableSphere.GetComponent<MeshRenderer>();
            _defaultSphereMaterial = _sphereMeshRenderer.material;
        }

        private void Start()
        {
            _inputProvider.MousePositionChanged += MoveConnectable;
            _inputProvider.MouseDown += CheckIfMovablePlatformCanBeSelected;
            _inputProvider.MouseUp += DeselectMovablePlatform;
            _inputProvider.MouseDown += CheckIfSelectableSphereCanBeSelected;
        }
        
        public void ChangeColor(ESelectingState selectingState)
        {
            _sphereMeshRenderer.material = selectingState switch
            {
                ESelectingState.Selecting => _isSelectableSphereSelected ? selectedMaterial : awaitingMaterial,
                ESelectingState.Unselected => _defaultSphereMaterial,
                _ => _sphereMeshRenderer.material
            };
        }
        
        private void MoveConnectable(Vector3 value)
        {
            if (_isMovablePlatformSelected)
            {
                gameObject.transform.position =
                    _camera.ScreenToWorldPoint(new Vector3(value.x, value.y, _camera.transform.position.y));
            }
        }

        private void CheckIfMovablePlatformCanBeSelected()
        {
            if (movablePlatform.IsCursorOverPlatform)
            {
                _isMovablePlatformSelected = true;
            }
        }

        private void DeselectMovablePlatform()
        {
            _isMovablePlatformSelected = false;
        }

        private void CheckIfSelectableSphereCanBeSelected()
        {
            if (!selectableSphere.IsCursorOverSphere) return;

            _isSelectableSphereSelected = true;
            _connectablesManager.ChangeColors(ESelectingState.Selecting);
        }
    }
}