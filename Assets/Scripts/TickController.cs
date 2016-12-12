using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickController : MonoBehaviour {

    public static TickController Obj;

    public delegate void TickEventHandler(float lengthOfTickSeconds);
    public static event TickEventHandler MoveTickEvent;
    public static event TickEventHandler ManipulateTickEvent;

    public delegate void ResetTabletsEventHandler();
    public static event ResetTabletsEventHandler ResetTabletsEvent;

    public float[] TicksPerSecond;

    private int newSpeedIndex = -1;
    private int speedIndex = -1;
    private float lastTickTimeSeconds = 0;

    void Awake() {
        if (Obj != null)
        {
            Destroy(this);
        }
        else
        {
            Obj = this;
        }
    }

    void Update() {
        speedIndex = newSpeedIndex;
        if (speedIndex >= 0) {
            if (Time.time - lastTickTimeSeconds >= 1 / TicksPerSecond[speedIndex]) {
                if (ManipulateTickEvent != null)
                    ManipulateTickEvent(1 / TicksPerSecond[speedIndex]);
                if (MoveTickEvent != null)
                    MoveTickEvent(1 / TicksPerSecond[speedIndex]);
                this.lastTickTimeSeconds = Time.time;
            }
        } else if (newSpeedIndex >= 0) {
            speedIndex = newSpeedIndex;
        }
    }

    public int GetMaxSpeed() {
        return TicksPerSecond.Length;
    }

    public void SetSpeed(int speed) {
        newSpeedIndex = Mathf.Clamp(speed - 1, -1, GetMaxSpeed());
    }

    public void ResetTablets() {
        if (ResetTabletsEvent != null)
            ResetTabletsEvent();
    }
}
