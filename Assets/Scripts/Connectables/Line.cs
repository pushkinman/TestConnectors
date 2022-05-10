using System.Linq;
using UnityEngine;

namespace TestConnectors.Connectables
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

        private void UpdateLinePoints()
        {
            var pointPositions = (from point in _connectionPoints select point.position).ToArray();
            _lineRenderer.SetPositions(pointPositions);
        }

        public void DestroyConnection()
        {
            Destroy(gameObject);
        }
    }
}