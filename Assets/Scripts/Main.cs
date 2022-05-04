using System;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;

namespace TestConnectors
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private float spawnRadius = ProjectSettings.SphereCount;
        [SerializeField] private int sphereCount = ProjectSettings.SphereCount;

        private IInputManager _inputManager;
        private IPlayerCamera _playerCamera;
        private ISpawner _spawner;

        private void Awake()
        {
            _inputManager = CompositionRoot.GetInputManager();
            _playerCamera = CompositionRoot.GetPlayerCamera();
            _spawner = CompositionRoot.GetSpawner();
        }

        private void Start()
        {
            _spawner.SpawnObjects(sphereCount, spawnRadius);
        }
    }
}
