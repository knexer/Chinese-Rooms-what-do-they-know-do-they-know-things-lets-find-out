﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGlow : MonoBehaviour {

  public TabletCell.Colors GlowColor;

  public tk2dSprite GlowSprite;

  void Awake () {
    if (GlowSprite != null) {
      switch (GlowColor) {
      case TabletCell.Colors.Blue:
        GlowSprite.color = Color.blue;
        break;
      case TabletCell.Colors.Green:
        GlowSprite.color = Color.green;
        break;
      default:
        GlowSprite.color = Color.white;
        break;
      }

    }
  }    
}