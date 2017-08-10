using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    //Parte do singleton
    //public static LevelManager levelManager;

    //Por meio desta variável que eu sei qual level eu preciso carregar
    public static int level = 0;

    private void Awake()
    {
        /**
         * Singleton
         *
        if (levelManager != null && levelManager != this)
        {
            Destroy(this.gameObject);
            return;
        }

        levelManager = this;
        DontDestroyOnLoad(gameObject);
        
         **/
    }


}
