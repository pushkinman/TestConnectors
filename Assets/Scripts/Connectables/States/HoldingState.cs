using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class HoldingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            connectablesSelectionSelectionStateManager.UpdateSpheres();
            connectablesSelectionSelectionStateManager.CursorConnection = connectablesSelectionSelectionStateManager.CreateConnection(
                connectablesSelectionSelectionStateManager.FirstSelectedSphere.transform,
                connectablesSelectionSelectionStateManager.PlayerCamera.CursorTransform);
        }

        public override void UpdateState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            HighlightPotentialSphere(connectablesSelectionSelectionStateManager);
            TryCreateAConnection(connectablesSelectionSelectionStateManager);
        }

        private void HighlightPotentialSphere(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            var ray = connectablesSelectionSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionSelectionStateManager
                .InputProvider.MousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var objectHit = hit.transform;
                var hitSphere = objectHit.GetComponent<SelectableSphere>();

                if (hitSphere != null)
                {
                    if (hitSphere.GetParentConnectable().IsSphereSelected == true) return;

                    connectablesSelectionSelectionStateManager.DeselectSecondSphere();
                    connectablesSelectionSelectionStateManager.SecondSelectedSphere = hitSphere;
                    connectablesSelectionSelectionStateManager.SecondSelectedSphere.GetParentConnectable().IsSphereSelected = true;

                    connectablesSelectionSelectionStateManager.UpdateSpheres();
                }
                else
                {
                    connectablesSelectionSelectionStateManager.DeselectSecondSphere();
                }
            }
        }

        private void TryCreateAConnection(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            var ray = connectablesSelectionSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionSelectionStateManager
                .InputProvider.MousePosition);

            if (connectablesSelectionSelectionStateManager.InputProvider.GetMouseButtonUp(0) == true)
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null)
                    {
                        connectablesSelectionSelectionStateManager.DestroyCursorConnection();
                        connectablesSelectionSelectionStateManager.DeselectSecondSphere();
                        connectablesSelectionSelectionStateManager.ChangeSelectionState(connectablesSelectionSelectionStateManager.UnselectedState);
                        return;
                    }

                    if (hitSphere.GetInstanceID() == connectablesSelectionSelectionStateManager.FirstSelectedSphere.GetInstanceID())
                    {
                        connectablesSelectionSelectionStateManager.DestroyCursorConnection();
                        connectablesSelectionSelectionStateManager.ChangeSelectionState(connectablesSelectionSelectionStateManager.ClickingState);
                        return;
                    }

                    connectablesSelectionSelectionStateManager.UpdateCursorConnectionPoints(
                        connectablesSelectionSelectionStateManager.FirstSelectedSphere.transform,
                        hitSphere.transform);
                    connectablesSelectionSelectionStateManager.DeselectFirstSphere();
                    connectablesSelectionSelectionStateManager.DeselectSecondSphere();

                    connectablesSelectionSelectionStateManager.ChangeSelectionState(connectablesSelectionSelectionStateManager.UnselectedState);
                }
                else
                {
                    connectablesSelectionSelectionStateManager.DestroyCursorConnection();
                    connectablesSelectionSelectionStateManager.ChangeSelectionState(connectablesSelectionSelectionStateManager.UnselectedState);
                }
            }
        }
    }
}