using System.Collections.Generic;
using TestConnectors.Enums;
using TestConnectors.Interfaces;
using TestConnectors.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestConnectors.Connectables
{
    public class ConnectablesSpawner : MonoBehaviour, IConnectablesSpawner
    {
        private List<Connectable> _connectables;

        private GameObject _sphereHolder;
        private IResourceManager _resourceManager;

        private void Awake()
        {
            _resourceManager = CompositionRoot.GetResourceManager();
            _connectables = new List<Connectable>();
            _sphereHolder = new GameObject(ProjectSettings.SphereHolderName);
        }

        public IEnumerable<Connectable> SpawnObjects(int count, float spawnRadius)
        {
            for (var i = 0; i < count; i++)
            {
                var connectable = _resourceManager.LoadResource<Connectable, EConnectable>(EConnectable.Connectable);
                _connectables.Add(connectable);
                var randomPosition = Random.insideUnitCircle * spawnRadius;
                connectable.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
                connectable.transform.SetParent(_sphereHolder.transform);
            }

            return _connectables;
        }
    }
}