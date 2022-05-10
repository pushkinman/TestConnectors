using UnityEngine;

namespace TestConnectors.Connectables.States
{
    public class ClickingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            connectablesSelectionStateManager.UpdateSpheres();
        }

        public override void UpdateState(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            if (connectablesSelectionStateManager.InputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionStateManager
                    .InputProvider.MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere != null &&
                        hitSphere.GetInstanceID() != connectablesSelectionStateManager.FirstSelectedSphere.GetInstanceID())
                    {
                        connectablesSelectionStateManager.CreateConnection(
                            connectablesSelectionStateManager.FirstSelectedSphere.transform,
                            hitSphere.transform);
                    }

                    connectablesSelectionStateManager.DeselectFirstSphere();
                    connectablesSelectionStateManager.ChangeSelectionState(connectablesSelectionStateManager.UnselectedState);
                }
            }
        }
    }
}