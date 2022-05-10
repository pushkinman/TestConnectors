using System;
using TestConnectors.Enums;
using TMPro;
using UnityEngine;

namespace TestConnectors.Connectable
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SelectableSphere : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void ChangeMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        public Connectable GetParentConnectable()
        {
            return transform.parent.GetComponent<Connectable>();
        }
    }
}