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
    public class ConnectablesStateManager : MonoBehaviour, IConnectablesManager
    {
        private readonly List<Connectable> _connectables = new List<Connectable>();
        private MovablePlatform _selectedPlatform;

        private GameObject _sphereHolder;
        private GameObject _connectionHolder;

        private IResourceManager _resourceManager;

        private BaseSelectionState _currentState;
        public readonly UnselectedState UnselectedState = new UnselectedState();
        public readonly HoldingState HoldingState = new HoldingState();
        public readonly ClickingState ClickingState = new ClickingState();

        public SelectableSphere SelectedSphere { get; set; }
        public IInputProvider InputProvider { get; private set; }
        public IPlayerCamera PlayerCamera { get; private set; }

        private void Awake()
        {
            _resourceManager = CompositionRoot.GetResourceManager();
            InputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            PlayerCamera = CompositionRoot.GetPlayerCamera();

            _sphereHolder = new GameObject(ProjectSettings.SphereHolderName);
            _connectionHolder = new GameObject(ProjectSettings.ConnectionHolderName);
        }

        private void Start()
        {
            _currentState = UnselectedState;

            InputProvider.MouseDown += CheckIfPlatformCanBeSelected;
            InputProvider.MouseUp += DeselectMovablePlatform;
            InputProvider.MouseStateChanged += TryMoveConnectable;
            InputProvider.MouseStateChanged += UpdateState;
        }

        private void OnDestroy()
        {
            InputProvider.MouseDown -= CheckIfPlatformCanBeSelected;
            InputProvider.MouseUp -= DeselectMovablePlatform;
            InputProvider.MouseStateChanged -= TryMoveConnectable;
            InputProvider.MouseStateChanged -= UpdateState;
        }

        private void UpdateState(Vector3 mousePosition)
        {
            _currentState.UpdateState(this);
        }

        public void SpawnObjects(int count, float spawnRadius)
        {
            for (var i = 0; i < count; i++)
            {
                var connectable = _resourceManager.LoadResource<Connectable, EConnectable>(EConnectable.Connectable);
                _connectables.Add(connectable);
                var randomPosition = Random.insideUnitCircle * spawnRadius;
                connectable.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
                connectable.transform.SetParent(_sphereHolder.transform);
            }
        }

        #region Platform Movement

        private void CheckIfPlatformCanBeSelected()
        {
            var ray = PlayerCamera.Camera.ScreenPointToRay(InputProvider.MousePosition);

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
                PlayerCamera.Camera.ScreenToWorldPoint(new Vector3(value.x, value.y,
                    PlayerCamera.Camera.transform.position.y));
        }

        #endregion

        public void DeselectSphere()
        {
            if (SelectedSphere == null) return;
            SelectedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = false;
            SelectedSphere = null;
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
            connection.transform.SetParent(_connectionHolder.transform);
            connection.SetConnectionPoints(point1, point2);
            return connection;
        }

        public void ChangeSelectionState(BaseSelectionState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }
    }
}