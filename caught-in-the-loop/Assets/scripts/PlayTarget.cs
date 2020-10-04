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
    public GameObject targetCircle;

    public Level[] levels;
    private int currentLevel;

    private void Start() {
        targetSoundLoop = GetComponent<TargetSoundLoop>();
        nextLevel();
    }

    
    private void nextLevel() {
        targetSoundLoop.level = levels[currentLevel++ % levels.Length];
        user.clearUserInput(targetSoundLoop.level, false);
        trigger();
    }

    public void trigger() {
        StartCoroutine(stop());
    }

    public IEnumerator stop() {
        user.enabled = false;
        targetCircle.SetActive(true);
        targetSoundLoop.line.localRotation = Quaternion.identity;
        
        yield return new WaitForSeconds(0.5f);
        
        foreach (var le in levels[0].loopElements) {
            while (le.audio.loadState == AudioDataLoadState.Loading) {
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        targetSoundLoop.enabled = true;

        yield return new WaitForSeconds(targetSoundLoop.level.loopTime * 2 - 0.05f);
        
        targetSoundLoop.line.localRotation = Quaternion.identity;
        targetCircle.SetActive(false);
        targetSoundLoop.enabled = false;
        
        yield return new WaitForSeconds(0.5f);

        user.enabled = true;
    }

    private void Update() {
        var statusText = TargetSoundLoop.compare(user.level.loopElements, targetSoundLoop.level.loopElements, targetSoundLoop.level.loopTime);

        if (statusText == "0 0") {
            nextLevel();
        }
        
        var parts = statusText.Split(' ');
        status.text = parts[0] + "  missing\n" + parts[1] + "  wrong";

        level.text = targetSoundLoop.level.name;
        
        if (Input.GetKeyDown(KeyCode.F)) {
            user.clearUserInput(targetSoundLoop.level, true);
        } else if (Input.GetKeyDown(KeyCode.N)) {
            nextLevel();
        }
    }
}