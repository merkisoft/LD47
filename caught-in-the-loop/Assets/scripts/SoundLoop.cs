using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLoop : MonoBehaviour {
    public Transform line;
    public Transform animationContainer; 

    public GameObject toneControlPrefab;
    public List<GameObject> toneControls = new List<GameObject>();
    public GameObject audioSourcePrefab;
    public GameObject pulsePrefab;
    
    private float startTime;
    private float lastTime;
    
    public Level level;

    private AudioSource[] sources = new AudioSource[10];
    private int sourcesIndex = 0;
    private SoundButton[] soundButtons;

    // Start is called before the first frame update
    public void Awake() {
        startTime = Time.time;
        lastTime = startTime;

        for (int i = 0; i < sources.Length; i++) {
            var audioSource = Instantiate(audioSourcePrefab);
            sources[i] = audioSource.GetComponent<AudioSource>();
        }
        
        soundButtons = GetComponentsInChildren<SoundButton>();
    }

    public void OnEnable() {
        startTime = Time.time;
        lastTime = startTime;
    }

    // Update is called once per frame
    void Update() {
        var time = (Time.time - startTime) % level.loopTime;

        if (line) line.localRotation = Quaternion.AngleAxis(360 / level.loopTime * time, Vector3.back);

        
        foreach (var le in level.loopElements) {
            if (le.time > lastTime && le.time < time
                || lastTime > time && le.time < time    // roll over, time = 0
                || lastTime > time && le.time > lastTime    // roll over, time = "< max"
                ) {
                  play(le);
            }
        }

        lastTime = time;
    }

    public void add(AudioSource source) {
        var time = (Time.time - startTime) % level.loopTime;

        var loopElement = new LoopElement(source.clip, time);
        play(loopElement);
        level.loopElements.Add(loopElement);

        var toneControl = Instantiate(toneControlPrefab, line.parent);
        toneControls.Add(toneControl);
        toneControl.transform.localRotation = Quaternion.AngleAxis(360 / level.loopTime * time, Vector3.back);
        toneControl.GetComponentInChildren<Button>().onClick.AddListener(() => {
            level.loopElements.Remove(loopElement);
            toneControls.Remove(toneControl);
            Destroy(toneControl);
        });
    }

    public virtual void clearUserInput(Level targetLevel, bool freeplay) {
        foreach (var g in toneControls) {
            Destroy(g);
        }

        level.loopTime = targetLevel.loopTime;
        level.loopElements.Clear();
        toneControls.Clear();

        foreach (var soundButton in soundButtons) {
            soundButton.gameObject.SetActive(freeplay);
            foreach (var le in targetLevel.loopElements) {
                if (le.audio == soundButton.clip.clip) {
                    soundButton.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
    
    private void play(LoopElement le) {
        var audioSource = sources[sourcesIndex++ % sources.Length];
        audioSource.clip = le.audio;
        audioSource.Play();

        if (pulsePrefab) {
            var instantiate = Instantiate(pulsePrefab, animationContainer);
            StartCoroutine(deletePulse(instantiate));
        }
    }

    IEnumerator deletePulse(GameObject pulse) {
        yield return new WaitForSeconds(0.5f);
        Destroy(pulse);
    }
}