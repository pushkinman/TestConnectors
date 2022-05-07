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
        private IConnectablesManager _connectablesManager;


        private void Awake()
        {
            _playerCamera = CompositionRoot.GetPlayerCamera();
            _connectablesManager = CompositionRoot.GetSpawner();
        }

        private void Start()
        {
            _connectablesManager.SpawnObjects(sphereCount, spawnRadius);
        }
    }
}
