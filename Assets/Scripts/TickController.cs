﻿using System;
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

    public delegate void ModeChangedHandler(TimeState newMode);
    public static event ModeChangedHandler ModeChangedEvent;

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
        if (!float.IsPositiveInfinity(SecondsPerTick()))
        {
            if (Time.time - lastTickTimeSeconds >= SecondsPerTick())
            {
                ToNextMode();
                if (!float.IsPositiveInfinity(SecondsPerTick()))
                {
                    if (ManipulateTickEvent != null)
                        ManipulateTickEvent(SecondsPerTick());
                    // Check Mode again in case any manipulators changed the speed
                    ToNextMode();
                    if (!float.IsPositiveInfinity(SecondsPerTick()) && MoveTickEvent != null)
                        MoveTickEvent(SecondsPerTick());
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

        if (CurrentMode != NextMode && ModeChangedEvent != null)
            ModeChangedEvent(NextMode);
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