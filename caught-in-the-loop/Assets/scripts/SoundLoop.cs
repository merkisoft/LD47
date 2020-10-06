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

    private float totalTime;
    public Text time;
    
    public Level level;
    private Level targetLevel;

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
        totalTime += Time.deltaTime;
        if (this.time && targetLevel) this.time.text = totalTime.ToString("0.00");
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
        var button = toneControl.GetComponentInChildren<Button>();
        if (targetLevel && targetLevel.matches(loopElement)) {
            button.image.color = Color.green;
        }
        toneControl.transform.localRotation = Quaternion.AngleAxis(360 / level.loopTime * time, Vector3.back);
        button.onClick.AddListener(() => {
            level.loopElements.Remove(loopElement);
            toneControls.Remove(toneControl);
            Destroy(toneControl);
        });
    }

    public virtual void clearUserInput(Level targetLevel) {
        bool freeplay = targetLevel == null;
        this.targetLevel = targetLevel;
        reset();

        level.loopTime = freeplay ? 4 : targetLevel.loopTime;

        foreach (var soundButton in soundButtons) {
            soundButton.gameObject.SetActive(freeplay);
            if (!freeplay) {
                foreach (var le in targetLevel.loopElements) {
                    if (le.audio == soundButton.clip.clip) {
                        soundButton.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }
    }

    public void reset() {
        time.text = "";
        totalTime = 0;
        
        foreach (var g in toneControls) {
            Destroy(g);
        }

        level.loopElements.Clear();
        toneControls.Clear();
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