using UnityEngine;

namespace TestConnectors.Connectables
{
    public class Connectable : MonoBehaviour
    {
        [SerializeField] private SelectableSphere selectableSphere;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Material awaitingMaterial;
        
        private Material _defaultSphereMaterial;
        private MeshRenderer _sphereMeshRenderer;

        public bool IsSphereSelected { get; set; }

        private void Awake()
        {
            _sphereMeshRenderer = selectableSphere.GetComponent<MeshRenderer>();
            _defaultSphereMaterial = _sphereMeshRenderer.material;
        }

        public void SetColoredMaterial()
        {
            selectableSphere.ChangeMaterial(IsSphereSelected ? selectedMaterial : awaitingMaterial);
        }

        public void SetDefaultMaterial()
        {
            selectableSphere.ChangeMaterial(_defaultSphereMaterial);
        }
    }
}