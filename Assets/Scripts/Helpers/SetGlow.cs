using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlow : MonoBehaviour {

  public TabletColor GlowColor;

  public tk2dSprite GlowSprite;

  void Awake () {
    if (GlowSprite != null) {
      switch (GlowColor) {
      case TabletColor.Blue:
        GlowSprite.color = Globals.Instance.BlueGlow;
        break;
      case TabletColor.Green:
        GlowSprite.color = Globals.Instance.GreenGlow;
        break;
      default:
        GlowSprite.color = Color.white;
        break;
      }

    }
  }    
}
