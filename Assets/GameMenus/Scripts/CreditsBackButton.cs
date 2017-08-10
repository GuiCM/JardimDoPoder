using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsBackButton : MenuControl, IPointerDownHandler {

	public override void OnPointerEnter (PointerEventData eventData)
	{
		this.GetComponent<Image>().sprite = base.textSpriteHover;
	}

	public override void OnPointerExit (PointerEventData eventData)
	{
		this.GetComponent<Image>().sprite = base.textSprite;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		SceneManager.LoadScene ("00MenuGame");
	}
}
