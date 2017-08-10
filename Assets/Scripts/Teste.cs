using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teste : MonoBehaviour {

    public void click()
    {
        LevelManager.level++;
        SceneManager.LoadScene("02Shop");
    }

    public void backclick()
    {
        SceneManager.LoadScene("01Game");
        print(LevelManager.level);
    }
}
