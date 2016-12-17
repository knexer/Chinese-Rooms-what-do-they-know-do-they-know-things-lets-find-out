using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimRunningUIMask : MonoBehaviour {
	
	void Update () {
        GetComponent<Image>().enabled = TickController.Obj.Mode != TickController.TimeState.Stopped;
	}
}
