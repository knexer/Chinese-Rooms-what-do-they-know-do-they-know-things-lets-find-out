using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BlackBoxButton : MonoBehaviour {

    public static BlackBoxButton Obj;

    public TabletUI Input;
    public TabletUI Output;

    // Use this for initialization
    void Start () {
        GetComponent<Button>().onClick.AddListener(UpdateOutput);
        Obj = this;
	}

    public void HideOutput() {
        SetOutputVisible(false);
    }
	
	private void UpdateOutput() {
        Output.SetState(Level.Obj.Evaluate(Input));
        SetOutputVisible(true);
    }

    private void SetOutputVisible(bool visible) {
        foreach (Transform child in Output.transform)
            foreach (Transform grandchild in child)
                grandchild.gameObject.SetActive(visible);
    }
}
