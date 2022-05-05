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
                var connectable = _resourceManager.LoadResource<TestConnectors.Connectable.Connectable, EConnectable>(EConnectable.Connectable);
                _connectables.Add(connectable);
                var randomPosition = Random.insideUnitCircle * spawnRadius;
                connectable.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
                connectable.transform.SetParent(sphereHolder.transform);
            }
        }

        public void ChangeColors(ESelectingState selectingState)
        {
            foreach (var connectable in _connectables)
            {
                connectable.ChangeColor(selectingState);
            }
        }
    }
}