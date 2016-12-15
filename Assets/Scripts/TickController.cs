using System;
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

    public delegate void SignalHandler();
    public static event SignalHandler OutOfBoundEvent;

    public void TriggerOutOfBounds()
    {
        OutOfBoundEvent();
    }

    public float RunningSecondsPerTick;
    public float FastForwardSecondsPerTick;

    public TimeState Mode
    {
        get { return CurrentMode; }
        set { NextMode = value; }
    }
    private TimeState CurrentMode = TimeState.Stopped;
    private TimeState NextMode = TimeState.Stopped;

    private float lastTickTimeSeconds = 0;

    void Awake() {
        if (Obj != null) {
            Destroy(this);
            return;
        }

        Obj = this;
        OutOfBoundEvent += Pause;
    }

    void Update() {
        float secondsPerTick = SecondsPerTick();

        if (!float.IsPositiveInfinity(secondsPerTick))
        {
            if (Time.time - lastTickTimeSeconds >= secondsPerTick)
            {
                ToNextMode();
                secondsPerTick = SecondsPerTick();
                if (!float.IsPositiveInfinity(secondsPerTick))
                {
                    if (ManipulateTickEvent != null)
                        ManipulateTickEvent(secondsPerTick);
                    if (MoveTickEvent != null)
                        MoveTickEvent(secondsPerTick);
                    this.lastTickTimeSeconds = Time.time;
                }
            }
        } else
        {
            ToNextMode();
        }
    }

    private void ToNextMode()
    {
        if (NextMode == TimeState.Stopped && CurrentMode != TimeState.Stopped)
        {
            // TODO(taylor): reset stuff
            ResetTablets();
        }
        CurrentMode = NextMode;
    }

    private float SecondsPerTick()
    {
        switch (Mode)
        {
            case TimeState.Stopped:
            case TimeState.Paused:
                return float.PositiveInfinity;
            case TimeState.Running:
                return RunningSecondsPerTick;
            case TimeState.FastForward:
                return FastForwardSecondsPerTick;
            case TimeState.MaximumWarp:
                return 0;
            default:
                throw new ArgumentException("Unexpected enum value " + Mode);
        }
    }

    void OnLevelWasLoaded() {
        Stop();
    }

    public void ResetTablets() {
        if (ResetTabletsEvent != null)
            ResetTabletsEvent();
    }

    public void Pause()
    {
        Mode = TimeState.Paused;
    }

    public void Stop()
    {
        Mode = TimeState.Stopped;
    }

    void OnDestroy() {
        OutOfBoundEvent -= Pause;
    }

    public enum TimeState
    {
        Stopped,
        Paused,
        Running,
        FastForward,
        MaximumWarp
    }
}