using System.Collections;
using System.Collections.Generic;
using MidiPlayerTK;
using UnityEngine;
using MidiPlayerTK;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    public MidiStreamPlayer player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
    }
    
    public void PlayOneNote(Slider slider) {
        var value = slider.value;
        
        // Play a note C5 for 1 second on the channel 0
        MPTKEvent NotePlaying = new MPTKEvent()
        {
            Command = MPTKCommand.NoteOn,
            Value = (int) value,  //C5
            Channel = 9,
            Duration = 1000,
            Velocity = 100,
            Delay = 0,
        };
        player.MPTK_PlayEvent(NotePlaying);
    }
}
