using System;
using TestConnectors.Enums;
using TMPro;
using UnityEngine;

namespace TestConnectors.Connectable
{
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
    }
}