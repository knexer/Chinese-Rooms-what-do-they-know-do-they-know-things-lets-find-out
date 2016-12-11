using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : Level {

    public override Tablet Evaluate(Tablet input) {
        Tablet output = Instantiate(input);
        return output;
    }
}
