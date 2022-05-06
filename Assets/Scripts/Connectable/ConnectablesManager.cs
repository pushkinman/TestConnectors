using System;
using System.Collections.Generic;
using TestConnectors.Enums;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestConnectors.Connectable
{
    public class ConnectablesManager : MonoBehaviour, IConnectablesManager
    {
        public EConnectable SelectingState { get; set; }

        private List<Connectable> _connectables = new List<Connectable>();
        private MovablePlatform _selectedPlatform;
        private SelectableSphere _selectedSphere;

        private IResourceManager _resourceManager;
        private IInputProvider _inputProvider;
        private Camera _camera;

        private void Awake()
        {
            _resourceManager = CompositionRoot.GetResourceManager();
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            _camera = CompositionRoot.GetPlayerCamera().Camera;
        }

        private void Start()
        {
            _inputProvider.MouseDown += CheckIfPlatformCanBeSelected;
            _inputProvider.MouseUp += DeselectMovablePlatform;
            _inputProvider.MousePositionChanged += TryMoveConnectable;
            _inputProvider.MouseDown += CheckIfSphereCanBeSelected;

        }

        public void SpawnObjects(int count, float spawnRadius)
        {
            var sphereHolder = new GameObject(ProjectSettings.SphereHolderName);
            for (var i = 0; i < count; i++)
            {
                var connectable = _resourceManager.LoadResource<Connectable, EConnectable>(EConnectable.Connectable);
                _connectables.Add(connectable);
                var randomPosition = Random.insideUnitCircle * spawnRadius;
                connectable.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
                connectable.transform.SetParent(sphereHolder.transform);
            }
        }

        #region Platform Movement

        private void CheckIfPlatformCanBeSelected()
        {
            var ray = _camera.ScreenPointToRay(_inputProvider.MousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var objectHit = hit.transform;
                _selectedPlatform = objectHit.GetComponent<MovablePlatform>();
            }
        }

        private void DeselectMovablePlatform()
        {
            _selectedPlatform = null;
        }

        private void TryMoveConnectable(Vector3 value)
        {
            if (_selectedPlatform == null) return;

            var connectable = _selectedPlatform.transform.parent;
            
            connectable.position =
                _camera.ScreenToWorldPoint(new Vector3(value.x, value.y, _camera.transform.position.y));
        }

        #endregion

        private void CheckIfSphereCanBeSelected()
        {
            var ray = _camera.ScreenPointToRay(_inputProvider.MousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var objectHit = hit.transform;
                var hitSphere = objectHit.GetComponent<SelectableSphere>();
                
                if (hitSphere == null)
                {
                    DeselectSphere();
                }
                else
                {
                    if (_selectedSphere != null)
                    {
                        CreateConnection(_selectedSphere.transform, hitSphere.transform);
                        DeselectSphere();
                    }
                    else
                    {
                        hitSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = true;
                        _selectedSphere = hitSphere;
                        UpdateSelectingState(ESelectingState.ClickingSelection);
                    }
                    
                }
            }
            else
            {
                DeselectSphere();
            }
        }

        private void DeselectSphere()
        {
            if (_selectedSphere == null) return;
            _selectedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = false;
            _selectedSphere = null;
            UpdateSelectingState(ESelectingState.Unselected);
        }

        private void UpdateSelectingState(ESelectingState selectingState)
        {
            if (selectingState == ESelectingState.ClickingSelection)
            {
                foreach (var connectable in _connectables)
                {
                    connectable.UpdateSphereMaterial();
                }
            }
            else if (selectingState == ESelectingState.Unselected)
            {
                foreach (var connectable in _connectables)
                {
                    connectable.ResetMaterial();
                }
            }
        }

        private void CreateConnection(Transform point1, Transform point2)
        {
            var connection = _resourceManager.LoadResource<Line, EConnection>(EConnection.RegularLine);
            connection.CreateLine(point1, point2);
        }
    }
}