using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLoop : MonoBehaviour {
    public Transform line;

    public GameObject toneControlPrefab;
    public GameObject audioSourcePrefab;
    
    private float startTime;
    private float lastTime;
    private float loopTime = 4;

    private List<LoopElement> loopElements = new List<LoopElement>();

    private AudioSource[] sources = new AudioSource[10];
    private int sourcesIndex = 0;
    
    class LoopElement {
        public AudioClip audio;
        public float time;

        public LoopElement(AudioClip audio, float time) {
            this.audio = audio;
            this.time = time;
        }

    }

    // Start is called before the first frame update
    void Start() {
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

        line.localRotation = Quaternion.AngleAxis(360 / loopTime * time, Vector3.back);

        foreach (var le in loopElements) {
            if (le.time > lastTime && le.time < time) {
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

        var toneControl = Instantiate(toneControlPrefab, transform);
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