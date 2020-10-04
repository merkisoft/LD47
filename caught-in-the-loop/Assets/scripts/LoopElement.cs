using System;
using UnityEngine;

public class LoopElement {
    public AudioClip audio;
    public float time;

    public LoopElement(AudioClip audio, float time) {
        this.audio = audio;
        this.time = time;
    }

    public bool matches(LoopElement loopElement, float loopTime) {
        if (audio != loopElement.audio) return false;

        var abs = Mathf.Abs(loopElement.time - time);
        var tolerance = 0.15f;
        if (abs > loopTime - tolerance) abs -= loopTime;

        return abs < tolerance;
    }
} 