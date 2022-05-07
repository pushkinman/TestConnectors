using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class ClickingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesManager connectablesManager)
        {
            connectablesManager.UpdateSpheres(true);
        }

        public override void UpdateState(ConnectablesManager connectablesManager)
        {

            if (connectablesManager._inputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesManager._camera.ScreenPointToRay(connectablesManager._inputProvider.MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere != null)
                    {
                        connectablesManager.CreateConnection(connectablesManager._selectedSphere.transform,
                            hitSphere.transform);
                    }

                    connectablesManager.DeselectSphere();
                    connectablesManager.ChangeSelectionState(connectablesManager.UnselectedState);
                }
            }
        }
    }
}