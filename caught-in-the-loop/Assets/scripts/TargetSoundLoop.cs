using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TargetSoundLoop : SoundLoop {
    public override void clearUserInput(Level level1) {
        // hide
    }

    public static string compare(List<LoopElement> _user, List<LoopElement> loopElements, float loopTime) {
        var target = new List<LoopElement>(loopElements);
        var user = new List<LoopElement>(_user);

        for (int i = 0; i < target.Count; i++) {
            for (int j = 0; j < user.Count; j++) {
                if (target[i].matches(user[j], loopTime)) {
                    target.RemoveAt(i--);
                    user.RemoveAt(j);
                    break;
                }
            }
        }

        var missing = target.Count;
        var additional = user.Count;

        return missing + " " + additional;
    }
}