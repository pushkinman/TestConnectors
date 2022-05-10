using UnityEngine;

namespace TestConnectors.Connectables.States
{
    public class UnselectedState : BaseSelectionState
    {
        public override void EnterState(ConnectablesSelectionStateManager connectablesSelectionStateManager)
        {
            connectablesSelectionStateManager.DeselectAllSpheres();
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

                    if (hitSphere == null) return;

                    hitSphere.GetParentConnectable().IsSphereSelected = true;
                    connectablesSelectionStateManager.FirstSelectedSphere = hitSphere;

                    connectablesSelectionStateManager.ChangeSelectionState(connectablesSelectionStateManager.HoldingState);
                }
            }
        }
    }
}