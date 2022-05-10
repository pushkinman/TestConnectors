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
    public class ConnectablesStateManager : MonoBehaviour, IConnectablesStateManager
    {
        public readonly UnselectedState UnselectedState = new UnselectedState();
        public readonly HoldingState HoldingState = new HoldingState();
        public readonly ClickingState ClickingState = new ClickingState();
        private BaseSelectionState _currentState;

        private readonly List<Connectable> _connectables = new List<Connectable>();

        private GameObject _sphereHolder;
        private GameObject _connectionHolder;

        private IResourceManager _resourceManager;
        public SelectableSphere FirstSelectedSphere { get; set; }
        public SelectableSphere SecondSelectedSphere { get; set; }
        public Line CursorConnection { get; set; }
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
            InputProvider.MouseStateChanged += UpdateState;
        }

        private void OnDestroy()
        {
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

        public void DeselectFirstSphere()
        {
            if (FirstSelectedSphere == null) return;
            FirstSelectedSphere.GetParentConnectable().IsSphereSelected = false;
            FirstSelectedSphere = null;
            UpdateSpheres();
        }

        public void DeselectSecondSphere()
        {
            if (SecondSelectedSphere == null) return;
            SecondSelectedSphere.GetParentConnectable().IsSphereSelected = false;
            SecondSelectedSphere = null;
            UpdateSpheres();
        }

        public void UpdateSpheres()
        {
            if (_currentState is HoldingState || _currentState is ClickingState)
            {
                foreach (var connectable in _connectables)
                {
                    connectable.SetColoredMaterial();
                }
            }
            else if (_currentState is UnselectedState)
            {
                foreach (var connectable in _connectables)
                {
                    connectable.SetDefaultMaterial();
                }
            }
        }

        public void DeselectAllSpheres()
        {
            foreach (var connectable in _connectables)
            {
                connectable.IsSphereSelected = false;
            }
        }

        public Line CreateConnection(Transform point1, Transform point2)
        {
            var connection = _resourceManager.LoadResource<Line, EConnection>(EConnection.RegularLine);
            connection.transform.SetParent(_connectionHolder.transform);
            connection.SetConnectionPoints(point1, point2);
            return connection;
        }

        public void UpdateCursorConnectionPoints(Transform point1, Transform point2)
        {
            if (CursorConnection != null)
            {
                CursorConnection.SetConnectionPoints(point1, point2);
            }
        }

        public void DestroyCursorConnection()
        {
            if (CursorConnection != null)
            {
                CursorConnection.DestroyConnection();
            }
        }

        public void ChangeSelectionState(BaseSelectionState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }
    }
}