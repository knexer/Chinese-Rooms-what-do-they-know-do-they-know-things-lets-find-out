using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {
  public Color BlueGlow = Color.blue;
  public Color GreenGlow = Color.green;

  public static Globals Instance { get; private set; }

  void Awake () {
    if (Instance != null) {
      Destroy(this.gameObject);
    }
    else {
      Instance = this;         
    }
  }
}
