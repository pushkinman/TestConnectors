using System;
using System.Collections.Generic;
using TestConnectors.Connectable.States;
using TestConnectors.Enums;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestConnectors.Connectable
{
    public class ConnectablesManager : MonoBehaviour, IConnectablesManager
    {
        private List<Connectable> _connectables = new List<Connectable>();
        private MovablePlatform _selectedPlatform;
        public SelectableSphere _selectedSphere;

        private IResourceManager _resourceManager;
        public IInputProvider _inputProvider;
        public IPlayerCamera _playerCamera;

        public BaseSelectionState CurrentState;
        public readonly UnselectedState UnselectedState = new UnselectedState();
        public HoldingState HoldingState = new HoldingState();
        public ClickingState ClickingState = new ClickingState();

        private void Awake()
        {
            _resourceManager = CompositionRoot.GetResourceManager();
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            _playerCamera = CompositionRoot.GetPlayerCamera();
        }

        private void Start()
        {
            CurrentState = UnselectedState;

            _inputProvider.MouseDown += CheckIfPlatformCanBeSelected;
            _inputProvider.MouseUp += DeselectMovablePlatform;
            _inputProvider.MouseStateChanged += TryMoveConnectable;
            _inputProvider.MouseStateChanged += (_) => CurrentState.UpdateState(this);
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
            var ray = _playerCamera.Camera.ScreenPointToRay(_inputProvider.MousePosition);

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
                _playerCamera.Camera.ScreenToWorldPoint(new Vector3(value.x, value.y,
                    _playerCamera.Camera.transform.position.y));
        }

        #endregion

        public void DeselectSphere()
        {
            if (_selectedSphere == null) return;
            _selectedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = false;
            _selectedSphere = null;
        }

        public void UpdateSpheres(bool isColored)
        {
            if (isColored)
            {
                foreach (var connectable in _connectables)
                {
                    connectable.UpdateSphereMaterial();
                }
            }
            else
            {
                foreach (var connectable in _connectables)
                {
                    connectable.ResetMaterial();
                }
            }
        }

        public Line CreateConnection(Transform point1, Transform point2)
        {
            var connection = _resourceManager.LoadResource<Line, EConnection>(EConnection.RegularLine);
            connection.SetConnectionPoints(point1, point2);
            return connection;
        }

        public void ChangeSelectionState(BaseSelectionState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }
    }
}