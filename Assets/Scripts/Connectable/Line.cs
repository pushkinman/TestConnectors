using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestConnectors.Connectable
{
    [RequireComponent(typeof(LineRenderer))]
    public class Line : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Transform[] _connectionPoints;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void CreateLine(params Transform[] connectionPoints)
        {
            _connectionPoints = connectionPoints;
        }

        private void Update()
        {
            if (_connectionPoints == null) return;
            _lineRenderer.SetPositions((from point in _connectionPoints select point.position).ToArray());
        }
    }
}