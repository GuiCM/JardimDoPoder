using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject defenderToInstantiate;

    public static GameObject selectedDefender;
    public static Button[] buttonArray;

    private void Start()
    {
        if (buttonArray == null)
            buttonArray = GameObject.FindObjectsOfType<Button>();

        if (!defenderToInstantiate)
            Debug.LogWarning("Botão " + name + " sem defensor para instanciar!");
    }

    private void OnMouseDown()
    {
        ClearSelectedButton();

        selectedDefender = defenderToInstantiate;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    public static void ClearSelectedButton()
    {
        foreach (Button button in buttonArray)
            button.GetComponentInChildren<SpriteRenderer>().color = new Color32(109, 109, 109, 255);
    }
}
