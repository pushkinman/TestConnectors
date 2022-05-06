using System;
using TestConnectors.Enums;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Connectable
{
    public class Connectable : MonoBehaviour
    {
        [SerializeField] private SelectableSphere selectableSphere;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material awaitingMaterial;
        
        private Material _defaultSphereMaterial;
        private MeshRenderer _sphereMeshRenderer;

        private IInputProvider _inputProvider;
        private Camera _camera;
        private IConnectablesManager _connectablesManager;
        
        public bool IsSphereSelected { get; set; }
        public Transform ConnectionPoint => selectableSphere.transform;

        private void Awake()
        {
            _inputProvider = CompositionRoot.GetInputManager().InputProviderInGame;
            _camera = CompositionRoot.GetPlayerCamera().Camera;
            _connectablesManager = CompositionRoot.GetSpawner();

            _sphereMeshRenderer = selectableSphere.GetComponent<MeshRenderer>();
            _defaultSphereMaterial = _sphereMeshRenderer.material;
        }

        public void UpdateSphereMaterial()
        {
            selectableSphere.ChangeMaterial(IsSphereSelected ? selectedMaterial : awaitingMaterial);
        }

        public void ResetMaterial()
        {
            selectableSphere.ChangeMaterial(_defaultSphereMaterial);
        }
    }
}