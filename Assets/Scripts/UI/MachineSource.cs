using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MachineSource : MonoBehaviour, IPointerDownHandler {

    public DraggableMachine MachinePrefab;

    public void OnPointerDown(PointerEventData eventData) {
        DraggableMachine newMachine = Instantiate(MachinePrefab);
        newMachine.StartDrag();
    }
}
