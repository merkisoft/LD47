using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SoundLoop : MonoBehaviour {
    public Transform line;

    public GameObject toneControlPrefab;
    public GameObject audioSourcePrefab;
    
    private float startTime;
    private float lastTime;
    private float loopTime = 2;

    public List<LoopElement> loopElements = new List<LoopElement>();

    private AudioSource[] sources = new AudioSource[10];
    private int sourcesIndex = 0;

    // Start is called before the first frame update
    public void Start() {
        startTime = Time.time;
        lastTime = startTime;

        for (int i = 0; i < sources.Length; i++) {
            var audioSource = Instantiate(audioSourcePrefab);
            sources[i] = audioSource.GetComponent<AudioSource>();
        }
        
    }


    // Update is called once per frame
    void Update() {
        var time = (Time.time - startTime) % loopTime;

        if (line) line.localRotation = Quaternion.AngleAxis(360 / loopTime * time, Vector3.back);

        
        foreach (var le in loopElements) {
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
        var time = (Time.time - startTime) % loopTime;

        var loopElement = new LoopElement(source.clip, time);
        play(loopElement);
        loopElements.Add(loopElement);

        var toneControl = Instantiate(toneControlPrefab, line.parent);
        toneControl.transform.localRotation = Quaternion.AngleAxis(360 / loopTime * time, Vector3.back);
        toneControl.GetComponentInChildren<Button>().onClick.AddListener(() => {
            loopElements.Remove(loopElement);
            Destroy(toneControl);
        });
    }

    private void play(LoopElement le) {
        var audioSource = sources[sourcesIndex++ % sources.Length];
        audioSource.clip = le.audio;
        audioSource.Play();
    }
}