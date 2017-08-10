using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtonCredits : MenuControl, IPointerDownHandler {

	//Mouse click
	public void OnPointerDown(PointerEventData eventData)
	{
		SceneManager.LoadScene ("00aCredits");
	}

}
