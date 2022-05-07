using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class HoldingState : BaseSelectionState
    {
        private Line _connection;
        private SelectableSphere _highlightedSphere;

        public override void EnterState(ConnectablesManager connectablesManager)
        {
            connectablesManager.UpdateSpheres(true);
            _connection = connectablesManager.CreateConnection(connectablesManager._selectedSphere.transform,
                connectablesManager._playerCamera.CursorTransform);
        }

        public override void UpdateState(ConnectablesManager connectablesManager)
        {
            HighlightPotentialSphere(connectablesManager);
            TryCreateAConnection(connectablesManager);
        }

        private void HighlightPotentialSphere(ConnectablesManager connectablesManager)
        {
            var ray = connectablesManager._playerCamera.Camera.ScreenPointToRay(connectablesManager._inputProvider
                .MousePosition);
            if (Physics.Raycast(ray, out var hit0))
            {
                var objectHit = hit0.transform;
                var hitSphere = objectHit.GetComponent<SelectableSphere>();

                if (hitSphere != null)
                {
                    if (hitSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected ==
                        false)
                    {
                        _highlightedSphere = hitSphere;
                        _highlightedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = true;
                        connectablesManager.UpdateSpheres(true);
                    }
                    
                }
                else
                {
                    ResetHighlightedSphere(connectablesManager);
                }
            }
        }

        private void TryCreateAConnection(ConnectablesManager connectablesManager)
        {
            var ray = connectablesManager._playerCamera.Camera.ScreenPointToRay(connectablesManager._inputProvider
                .MousePosition);

            if (connectablesManager._inputProvider.GetMouseButtonUp(0) == true)
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null)
                    {
                        _connection.DestroyConnection();
                        ResetHighlightedSphere(connectablesManager);
                        connectablesManager.ChangeSelectionState(connectablesManager.UnselectedState);
                        return;
                    }

                    if (hitSphere.GetInstanceID() == connectablesManager._selectedSphere.GetInstanceID())
                    {
                        _connection.DestroyConnection();
                        connectablesManager.ChangeSelectionState(connectablesManager.ClickingState);
                        return;
                    }

                    _connection.SetConnectionPoints(connectablesManager._selectedSphere.transform,
                        hitSphere.transform);
                    connectablesManager.DeselectSphere();
                    ResetHighlightedSphere(connectablesManager);

                    connectablesManager.ChangeSelectionState(connectablesManager.UnselectedState);
                }
                else
                {
                    _connection.DestroyConnection();
                    connectablesManager.ChangeSelectionState(connectablesManager.UnselectedState);
                }
            }
        }

        private void ResetHighlightedSphere(ConnectablesManager connectablesManager)
        {
            if (_highlightedSphere != null)
            {
                _highlightedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = false;
                connectablesManager.UpdateSpheres(true);
                _highlightedSphere = null;
            }
        }
    }
}