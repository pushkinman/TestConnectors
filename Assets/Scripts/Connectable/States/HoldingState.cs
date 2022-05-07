using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class HoldingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesStateManager connectablesStateManager)
        {
            connectablesStateManager.UpdateSpheres(true);
            connectablesStateManager.CursorConnection = connectablesStateManager.CreateConnection(connectablesStateManager.FirstSelectedSphere.transform,
                connectablesStateManager.PlayerCamera.CursorTransform);
        }

        public override void UpdateState(ConnectablesStateManager connectablesStateManager)
        {
            HighlightPotentialSphere(connectablesStateManager);
            TryCreateAConnection(connectablesStateManager);
        }

        private void HighlightPotentialSphere(ConnectablesStateManager connectablesStateManager)
        {
            var ray = connectablesStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesStateManager
                .InputProvider.MousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var objectHit = hit.transform;
                var hitSphere = objectHit.GetComponent<SelectableSphere>();

                if (hitSphere != null)
                {
                    if (hitSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected == true) return;
                    
                    connectablesStateManager.SecondSelectedSphere = hitSphere;
                    connectablesStateManager.SecondSelectedSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = true;
                    connectablesStateManager.UpdateSpheres(true);
                }
                else
                {
                    connectablesStateManager.DeselectSecondSphere();
                }
            }
        }

        private void TryCreateAConnection(ConnectablesStateManager connectablesStateManager)
        {
            var ray = connectablesStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesStateManager
                .InputProvider.MousePosition);

            if (connectablesStateManager.InputProvider.GetMouseButtonUp(0) == true)
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null)
                    {
                        connectablesStateManager.DestroyCursorConnection();
                        connectablesStateManager.DeselectSecondSphere();
                        connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                        return;
                    }

                    if (hitSphere.GetInstanceID() == connectablesStateManager.FirstSelectedSphere.GetInstanceID())
                    {
                        connectablesStateManager.DestroyCursorConnection();
                        connectablesStateManager.ChangeSelectionState(connectablesStateManager.ClickingState);
                        return;
                    }

                    connectablesStateManager.UpdateCursorConnectionPoints(connectablesStateManager.FirstSelectedSphere.transform,
                        hitSphere.transform);
                    connectablesStateManager.DeselectFirstSphere();
                    connectablesStateManager.DeselectSecondSphere();

                    connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                }
                else
                {
                    connectablesStateManager.DestroyCursorConnection();
                    connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                }
            }
        }
    }
}