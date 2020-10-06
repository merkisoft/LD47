using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject {
    public float loopTime;
    public List<LoopElement> loopElements = new List<LoopElement>();

    public string compare(Level targetLevel) {
        var target = new List<LoopElement>(targetLevel.loopElements);
        var user = new List<LoopElement>(loopElements);

        for (int i = 0; i < target.Count; i++) {
            for (int j = 0; j < user.Count; j++) {
                if (target[i].matches(user[j], targetLevel.loopTime)) {
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

    public bool matches(LoopElement loopElement) {
        for (int i = 0; i < loopElements.Count; i++) {
            if (loopElements[i].matches(loopElement, loopTime)) {
                return true;
            }
        }

        return false;
    }
}