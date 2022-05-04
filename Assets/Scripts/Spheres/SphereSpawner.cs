using System;
using TestConnectors.Enums;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestConnectors.Spheres
{
    public class SphereSpawner : MonoBehaviour, ISpawner
    {
        private IResourceManager _resourceManager;

        private void Awake()
        {
            _resourceManager = CompositionRoot.GetResourceManager();
        }

        public void SpawnObjects(int count, float spawnRadius)
        {
            var sphereHolder = new GameObject(ProjectSettings.SphereHolderName);
            for (var i = 0; i < count; i++)
            {
                var obj = _resourceManager.LoadResource<ConnectableSphere, EConnectable>(EConnectable.Connectable);
                var randomPosition = Random.insideUnitCircle * spawnRadius;
                obj.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
                obj.transform.SetParent(sphereHolder.transform);
            }
        }
    }
}