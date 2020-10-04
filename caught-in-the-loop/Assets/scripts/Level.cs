using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 1)]

public class Level : ScriptableObject {
    public float loopTime;
    public List<LoopElement> loopElements = new List<LoopElement>();

} 