using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class ClickingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            connectablesSelectionSelectionStateManager.UpdateSpheres();
        }

        public override void UpdateState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            if (connectablesSelectionSelectionStateManager.InputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesSelectionSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionSelectionStateManager
                    .InputProvider.MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere != null &&
                        hitSphere.GetInstanceID() != connectablesSelectionSelectionStateManager.FirstSelectedSphere.GetInstanceID())
                    {
                        connectablesSelectionSelectionStateManager.CreateConnection(
                            connectablesSelectionSelectionStateManager.FirstSelectedSphere.transform,
                            hitSphere.transform);
                    }

                    connectablesSelectionSelectionStateManager.DeselectFirstSphere();
                    connectablesSelectionSelectionStateManager.ChangeSelectionState(connectablesSelectionSelectionStateManager.UnselectedState);
                }
            }
        }
    }
}