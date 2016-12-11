using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThree : Level {

    public override Tablet Evaluate(Tablet input) {
        Tablet output = Instantiate(input);
        return output;
    }
}
