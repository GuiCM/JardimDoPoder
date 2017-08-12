using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public int Number { get; set; }
    public int AttackersAmount { get; set; }
    public int MinLineToSpawn { get; set; }
    public int MaxLineToSpawn { get; set; }
    public float AttackerTimeRateMin { get; set; }
    public float AttackerTimeRateMax { get; set; }
    public List<string> AttackersNameCollection;
}