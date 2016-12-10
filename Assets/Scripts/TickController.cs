using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickController : MonoBehaviour {

    public static TickController Obj;

    public delegate void TickEventHandler(float lengthOfTickSeconds);
    public static event TickEventHandler TickEvent;

    public delegate void ResetTabletsEventHandler();
    public static event ResetTabletsEventHandler ResetTabletsEvent;

    public float[] TicksPerSecond;

    private int speedIndex = -1;
    private float lastTickTimeSeconds = 0;

    void Awake() {
        if (Obj != null)
        {
            Destroy(this);
        }
        Obj = this;
    }

    void Update() {
        if (speedIndex >= 0 
            && Time.time - lastTickTimeSeconds >= 1 / TicksPerSecond[speedIndex]
            && TickEvent != null)
        {
            TickEvent(1 / TicksPerSecond[speedIndex]);
        }
    }

    public int GetMaxSpeed() {
        return TicksPerSecond.Length;
    }

    public void SetSpeed(int speed) {
        speedIndex = Mathf.Clamp(speed - 1, 0, GetMaxSpeed());
        lastTickTimeSeconds = Time.time;
    }

    public void ResetTablets() {
        if (ResetTabletsEvent != null)
            ResetTabletsEvent();
    }
}
