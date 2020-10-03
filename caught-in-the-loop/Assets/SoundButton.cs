using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

    public AudioSource clip;
    private SoundLoop soundLoop;

    public void Start() {
        soundLoop = GetComponentInParent<SoundLoop>();
    }

    public void trigger() {
        soundLoop.add(clip);
    }
}
