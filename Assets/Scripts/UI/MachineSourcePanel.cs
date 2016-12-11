using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSourcePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (MachineSource prefab in Level.Obj.availableMachines) {
            MachineSource instance = Instantiate(prefab);
            instance.transform.SetParent(this.transform);
            instance.transform.localScale = Vector3.one;
        }
	}
}
