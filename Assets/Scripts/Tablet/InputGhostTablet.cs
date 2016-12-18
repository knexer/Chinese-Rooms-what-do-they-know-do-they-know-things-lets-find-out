using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameTabletRenderer))]
public class InputGhostTablet : MonoBehaviour {
    
	void Start () {
        Vector2 position = MachineGrid.Obj.getVertexWorldPosition(
            MachineGrid.Obj.GridTablet.startGridVertexX, MachineGrid.Obj.GridTablet.startGridVertexY);
        transform.position = new Vector3(position.x, position.y, transform.position.z);

        LevelStateManager.InputChanged += UpdateState;
    }

    void OnDestroy() {
        LevelStateManager.InputChanged -= UpdateState;
    }

    private void UpdateState(ITablet state) {
        GetComponent<GameTabletRenderer>().SetState(state);
    }
}
