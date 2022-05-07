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

        private void Update()
        {
            if (_connectionPoints == null) return;
            UpdateLinePoints();
        }
        
        public void SetConnectionPoints(params Transform[] connectionPoints)
        {
            _connectionPoints = connectionPoints;
            UpdateLinePoints();
        }

        public void DestroyConnection()
        {
            Destroy(gameObject);
        }

        private void UpdateLinePoints()
        {
            var pointPositions = (from point in _connectionPoints select point.position).ToArray();
            _lineRenderer.SetPositions(pointPositions);
        }
    }
}