using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private LevelInfo[] levelInfoCollection;
    //Por meio desta variável que eu sei qual level eu preciso carregar
    private int levelIndex = 0;

    //Parte do singleton
    public static LevelManager levelManager;

    private void Awake()
    {
        if (levelManager != null && levelManager != this)
        {
            Destroy(this.gameObject);
            return;
        }

        levelManager = this;
        DontDestroyOnLoad(gameObject);
        InitializeLevelCollection();
    }

    private void InitializeLevelCollection()
    {
        levelInfoCollection = new LevelInfo[3];

        LevelInfo levelInfo = new LevelInfo()
        {
            Number = 1,
            AttackersAmount = 4,
            MinLineToSpawn = 2,
            MaxLineToSpawn = 4,
            AttackerTimeRateMin = 10f,
            AttackerTimeRateMax = 15f
        };
        levelInfo.AttackersNameCollection = new List<string>();
        levelInfo.AttackersNameCollection.Add("Lizard");
        levelInfo.AttackersNameCollection.Add("Fox");

        levelInfoCollection[0] = levelInfo;

        levelInfo = new LevelInfo()
        {
            Number = 2,
            AttackersAmount = 5,
            MinLineToSpawn = 2,
            MaxLineToSpawn = 4,
            AttackerTimeRateMin = 9f,
            AttackerTimeRateMax = 13f
        };

        levelInfo.AttackersNameCollection = new List<string>();
        levelInfo.AttackersNameCollection.Add("Lizard");
        levelInfo.AttackersNameCollection.Add("Fox");

        levelInfoCollection[1] = levelInfo;

        levelInfo = new LevelInfo()
        {
            Number = 3,
            AttackersAmount = 6,
            MinLineToSpawn = 2,
            MaxLineToSpawn = 4,
            AttackerTimeRateMin = 8f,
            AttackerTimeRateMax = 12f
        };

        levelInfo.AttackersNameCollection = new List<string>();
        levelInfo.AttackersNameCollection.Add("Lizard");
        levelInfo.AttackersNameCollection.Add("Fox");

        levelInfoCollection[2] = levelInfo;

    }

    public LevelInfo NextLevelInformations()
    {
        return levelInfoCollection[levelIndex];
    }
}
