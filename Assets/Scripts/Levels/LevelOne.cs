using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : Level {

    public override Tablet Evaluate(Tablet input) {
        return Instantiate(input);
    }
}
