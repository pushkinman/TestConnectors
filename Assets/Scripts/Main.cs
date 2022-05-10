using System;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;

namespace TestConnectors
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private float spawnRadius = ProjectSettings.SpawnRadius;
        [SerializeField] private int sphereCount = ProjectSettings.SphereCount;

        private IPlayerCamera _playerCamera;
        private IConnectablesSpawner _connectablesSpawner;
        private IConnectablesSelectionStateManager _connectablesSelectionStateManager;
        private IConnectablesMover _connectablesMover;

        private void Awake()
        {
            _playerCamera = CompositionRoot.GetPlayerCamera();
            _connectablesSpawner = CompositionRoot.GetConnectablesSpawner();
            _connectablesSelectionStateManager = CompositionRoot.GetConnectablesStateManager();
            _connectablesMover = CompositionRoot.GetConnectablesMover();
        }

        private void Start()
        {
            _connectablesSelectionStateManager.Connectables =
                _connectablesSpawner.SpawnObjects(sphereCount, spawnRadius);
        }
    }
}