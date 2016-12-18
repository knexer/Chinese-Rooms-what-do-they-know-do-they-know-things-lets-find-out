using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MachineSource : MonoBehaviour, IPointerDownHandler {

    public MachineType type;
    public DraggableMachine MachinePrefab;

    private bool available = false;

    void Start() {
        available = Level.Obj.availableMachines.Contains(type);
        float alpha = available ? 1 : 0.2f;
        foreach (Image i in GetComponentsInChildren<Image>()) {
            Color newColor = new Color(i.color.r, i.color.g, i.color.b, alpha);
            i.color = newColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (available) {
            DraggableMachine newMachine = Instantiate(MachinePrefab);
            newMachine.StartDrag();
        }
    }
}
