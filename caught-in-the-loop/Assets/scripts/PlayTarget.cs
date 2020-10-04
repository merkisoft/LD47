using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTarget : MonoBehaviour {
    private TargetSoundLoop targetSoundLoop;

    public SoundLoop user;

    public Text status;

    private void Start() {
        targetSoundLoop = GetComponent<TargetSoundLoop>();
        
        trigger();
    }

    public void trigger() {
        StartCoroutine(stop());
    }

    public IEnumerator stop() {
        targetSoundLoop.enabled = true;
        user.enabled = false;
        
        yield return new WaitForSeconds(targetSoundLoop.loopTime);

        targetSoundLoop.enabled = false;

        yield return new WaitForSeconds(0.5f);

        user.enabled = true;
    }

    private void Update() {
        status.text = TargetSoundLoop.compare(user.loopElements, targetSoundLoop.loopElements, targetSoundLoop.loopTime);
    }
}