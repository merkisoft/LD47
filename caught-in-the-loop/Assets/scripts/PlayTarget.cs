using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTarget : MonoBehaviour {
    private TargetSoundLoop targetSoundLoop;

    public SoundLoop user;

    public Text status;
    public Text level;

    public Level[] levels;
    private int currentLevel;

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
        
        yield return new WaitForSeconds(targetSoundLoop.level.loopTime);

        targetSoundLoop.enabled = false;

        yield return new WaitForSeconds(0.5f);

        user.enabled = true;
    }

    private void Update() {
        var statusText = TargetSoundLoop.compare(user.level.loopElements, targetSoundLoop.level.loopElements, targetSoundLoop.level.loopTime);

        if (statusText == "0 0") {
            targetSoundLoop.level = levels[++currentLevel % levels.Length];
            user.clearUserInput();
            trigger();
        }
        
        var parts = statusText.Split(' ');
        status.text = "missing: " + parts[0] + " | wrong: " + parts[1];

        level.text = targetSoundLoop.level.name;
    }
}