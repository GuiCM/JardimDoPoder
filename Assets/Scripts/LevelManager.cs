using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private LevelInfo[] levelInfoCollection;
    //Parte do singleton
    public static LevelManager levelManager;

    //Por meio desta variável que eu sei qual level eu preciso carregar
    public int level = 0;

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
        levelInfoCollection = new LevelInfo[10];

    }

    public LevelInfo NextLevelInformations()
    {
        return levelInfoCollection[level];
    }
}
