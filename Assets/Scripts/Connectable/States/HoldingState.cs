using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class HoldingState : BaseSelectionState
    {
        private Line _connection;
        private SelectableSphere _highlightedSphere;

        public override void EnterState(ConnectablesStateManager connectablesStateManager)
        {
            connectablesStateManager.UpdateSpheres(true);
            _connection = connectablesStateManager.CreateConnection(connectablesStateManager.SelectedSphere.transform,
                connectablesStateManager.PlayerCamera.CursorTransform);
        }

        public override void UpdateState(ConnectablesStateManager connectablesStateManager)
        {
            HighlightPotentialSphere(connectablesStateManager);
            TryCreateAConnection(connectablesStateManager);
        }

        private void HighlightPotentialSphere(ConnectablesStateManager connectablesStateManager)
        {
            var ray = connectablesStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesStateManager.InputProvider
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
                        connectablesStateManager.UpdateSpheres(true);
                    }
                    
                }
                else
                {
                    ResetHighlightedSphere(connectablesStateManager);
                }
            }
        }

        private void TryCreateAConnection(ConnectablesStateManager connectablesStateManager)
        {
            var ray = connectablesStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesStateManager.InputProvider
                .MousePosition);

            if (connectablesStateManager.InputProvider.GetMouseButtonUp(0) == true)
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null)
                    {
                        _connection.DestroyConnection();
                        ResetHighlightedSphere(connectablesStateManager);
                        connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                        return;
                    }

                    if (hitSphere.GetInstanceID() == connectablesStateManager.SelectedSphere.GetInstanceID())
                    {
                        _connection.DestroyConnection();
                        connectablesStateManager.ChangeSelectionState(connectablesStateManager.ClickingState);
                        return;
                    }

                    _connection.SetConnectionPoints(connectablesStateManager.SelectedSphere.transform,
                        hitSphere.transform);
                    connectablesStateManager.DeselectSphere();
                    ResetHighlightedSphere(connectablesStateManager);

                    connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                }
                else
                {
                    _connection.DestroyConnection();
                    connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                }
            }
        }

        private void ResetHighlightedSphere(ConnectablesStateManager connectablesStateManager)
        {
            if (_highlightedSphere != null)
            {
                _highlightedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = false;
                connectablesStateManager.UpdateSpheres(true);
                _highlightedSphere = null;
            }
        }
    }
}