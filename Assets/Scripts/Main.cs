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
        private IConnectablesStateManager _connectablesStateManager;
        private IConnectablesMover _connectablesMover;

        private void Awake()
        {
            _playerCamera = CompositionRoot.GetPlayerCamera();
            _connectablesStateManager = CompositionRoot.GetConnectablesStateManager();
            _connectablesMover = CompositionRoot.GetConnectablesMover();
        }

        private void Start()
        {
            _connectablesStateManager.SpawnObjects(sphereCount, spawnRadius);
        }
    }
}
