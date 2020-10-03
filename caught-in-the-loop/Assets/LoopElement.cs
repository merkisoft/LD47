using UnityEngine;

public class LoopElement {
    public AudioClip audio;
    public float time;

    public LoopElement(AudioClip audio, float time) {
        this.audio = audio;
        this.time = time;
    }
}