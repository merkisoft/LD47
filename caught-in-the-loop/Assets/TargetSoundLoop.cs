using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TargetSoundLoop : SoundLoop {
    public AudioSource[] audioSources;
    
    // Start is called before the first frame update
    void Start() {
        base.Start();

        loopElements.Add(new LoopElement(audioSources[0].clip, 0f));
        loopElements.Add(new LoopElement(audioSources[1].clip, 0f));
        loopElements.Add(new LoopElement(audioSources[0].clip, 0.5f));
        loopElements.Add(new LoopElement(audioSources[0].clip, 1f));
        loopElements.Add(new LoopElement(audioSources[2].clip, 1f));
        loopElements.Add(new LoopElement(audioSources[0].clip, 1.5f));
        loopElements.Add(new LoopElement(audioSources[3].clip, 1.999f));

    }

}
