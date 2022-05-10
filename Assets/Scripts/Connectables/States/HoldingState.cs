using UnityEngine;

namespace TestConnectors.Connectables.States
{
    public class HoldingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            connectablesSelectionStateManager.UpdateSpheres();
            connectablesSelectionStateManager.CursorConnection = connectablesSelectionStateManager.CreateConnection(
                connectablesSelectionStateManager.FirstSelectedSphere.transform,
                connectablesSelectionStateManager.PlayerCamera.CursorTransform);
        }

        public override void UpdateState(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            HighlightPotentialSphere(connectablesSelectionStateManager);
            TryCreateAConnection(connectablesSelectionStateManager);
        }

        private void HighlightPotentialSphere(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            var ray = connectablesSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionStateManager
                .InputProvider.MousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                var objectHit = hit.transform;
                var hitSphere = objectHit.GetComponent<SelectableSphere>();

                if (hitSphere != null)
                {
                    if (hitSphere.GetParentConnectable().IsSphereSelected == true) return;

                    connectablesSelectionStateManager.DeselectSecondSphere();
                    connectablesSelectionStateManager.SecondSelectedSphere = hitSphere;
                    connectablesSelectionStateManager.SecondSelectedSphere.GetParentConnectable().IsSphereSelected = true;

                    connectablesSelectionStateManager.UpdateSpheres();
                }
                else
                {
                    connectablesSelectionStateManager.DeselectSecondSphere();
                }
            }
        }

        private void TryCreateAConnection(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            var ray = connectablesSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionStateManager
                .InputProvider.MousePosition);

            if (connectablesSelectionStateManager.InputProvider.GetMouseButtonUp(0) == true)
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null)
                    {
                        connectablesSelectionStateManager.DestroyCursorConnection();
                        connectablesSelectionStateManager.DeselectSecondSphere();
                        connectablesSelectionStateManager.ChangeSelectionState(connectablesSelectionStateManager.UnselectedState);
                        return;
                    }

                    if (hitSphere.GetInstanceID() == connectablesSelectionStateManager.FirstSelectedSphere.GetInstanceID())
                    {
                        connectablesSelectionStateManager.DestroyCursorConnection();
                        connectablesSelectionStateManager.ChangeSelectionState(connectablesSelectionStateManager.ClickingState);
                        return;
                    }

                    connectablesSelectionStateManager.UpdateCursorConnectionPoints(
                        connectablesSelectionStateManager.FirstSelectedSphere.transform,
                        hitSphere.transform);
                    connectablesSelectionStateManager.DeselectFirstSphere();
                    connectablesSelectionStateManager.DeselectSecondSphere();

                    connectablesSelectionStateManager.ChangeSelectionState(connectablesSelectionStateManager.UnselectedState);
                }
                else
                {
                    connectablesSelectionStateManager.DestroyCursorConnection();
                    connectablesSelectionStateManager.ChangeSelectionState(connectablesSelectionStateManager.UnselectedState);
                }
            }
        }
    }
}