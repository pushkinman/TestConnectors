using TestConnectors.Enums;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class UnselectedState : BaseSelectionState
    {
        public override void EnterState(ConnectablesManager connectablesManager)
        {
            connectablesManager.UpdateSpheres(false);
        }

        public override void UpdateState(ConnectablesManager connectablesManager)
        {
            if (connectablesManager._inputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesManager._playerCamera.Camera.ScreenPointToRay(connectablesManager._inputProvider
                    .MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null) return;

                    hitSphere.transform.parent.GetComponent<Connectable>().IsSphereSelected = true;
                    connectablesManager._selectedSphere = hitSphere;

                    connectablesManager.ChangeSelectionState(connectablesManager.HoldingState);
                }
            }
        }
    }
}